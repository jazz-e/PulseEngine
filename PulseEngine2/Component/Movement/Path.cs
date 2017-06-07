using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;
using PulseEngine.Display.World;

namespace PulseEngine.Component.Movement
{
    public enum PathStatus { Forward, Reverse, Stop}
    
    public class FollowPath : IEntityComponent, IEntityInitialiseComponent, IEntityUpdateComponent
    {
        Entity _parent;
        int _index = 0;

        public bool Loop
        { get; set; }

        public float Speed
        { get; set; }

        public Vector2 Target
        { get; set; }
    
        public PathStatus Status
        { get; set;}
       
        public Vector2[] Path;

        void NextStep(GameTime gameTime)
        {
            //If looping through array 
            if (Loop)
            {
                if (Status == PathStatus.Forward && _index == this.Path.Length - 1)
                { _index = 0; Target = this.Path[_index]; return; }

                if (Status == PathStatus.Reverse && _index == 0)
                { _index = this.Path.Length - 1; Target = this.Path[_index]; return; }
            }

            //If moving forward through array
            if (_index < this.Path.Length - 1 && Status == PathStatus.Forward) _index++;
            //It move backward through array 
            if (_index > 0 && Status == PathStatus.Reverse) _index--;

            Target = this.Path[_index];
        }

        public void Step(GameTime gameTime)
        {
            float _delta = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Vector2 step =
                StepTowards.Step(_delta,
                _parent.Position, Target);
            
             _parent.Position +=
                new Vector2((int)step.X, (int)step.Y);

            //If Entity reach Target then look for Next Target 

            if (Math.Ceiling(_parent.X + (int)_delta) >= Target.X && 
                Math.Floor(_parent.X - (int)_delta) <= Target.X)
            {
                if (Math.Ceiling(_parent.Y +_delta) >= Target.Y && 
                    Math.Floor(_parent.Y  - _delta) <= Target.Y)
                {
                    NextStep(gameTime);
                }
            }
                // --------------------------------------------------    
        }

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }

        public void Update(GameTime gameTime)
        {
            Step(gameTime);       
        }

        public void Initialise()
        {
            Target = Path[_index];
        }
    }

    public struct SquareNode
    {
        public int Identification; // Node ID 
        public int ParentIdentification; //Parent Node 
        public int H, G, F; // attributes used for score
        public Vector2 Location; // Position in Grid  
    }

    public class FindPath : IEntityComponent, IEntityInitialiseComponent, IEntityUpdateComponent
    {
        Entity _parent;
        int _columns;

        int stepFromStart;
        int _index = 0;
        
        List<SquareNode> _closedList,
            _openList, _path;

        public Vector2 TargetScreenLocation
        {
            get;set;
        }

        Vector2 TargetSquare { get; set;}

        public float Speed
        { get; set; }

        public Vector2 Target
        { get; set; }

        public TileMap Level
        { get; set; }

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }

        Vector2 _lastTarget; 

        public void Update(GameTime gameTime)
        {
            if (_path == null)
            {
               if(FindAPath().Count == 0) return;
            }
            
            if (_path.Count != _index)
            {    
                Step(gameTime);
            }

            _lastTarget = Target;
        }

        public void Initialise()
        {
            if(this.Level != null)
            _columns = this.Level.MapWidth;
        }

        //Generate a Path 
        public List<SquareNode> FindAPath()
        {
            SquareNode squareNode =new SquareNode(); //Temp Square Node
            SquareNode currentSquare;

            _openList = new List<SquareNode>();
            _closedList = new List<SquareNode>();

            squareNode.Location = GridReference(_parent.Position); // Get Grid Ref 
            squareNode.Identification = GetGridID(squareNode.Location); // Get ID      
            squareNode.ParentIdentification = -1; // Parent ID - Original
            squareNode.F = this.Level.Tiles.Count; //Get the highest number of Squares
            squareNode.G = 0; // No Position - shows the start location
            squareNode.H = 0; // Estimate - squares 

            TargetSquare = GridReference(this.TargetScreenLocation); // Target Square;

            _openList.Add(squareNode);

            stepFromStart = 0;

            do
            {
                if (_openList.Count > 1)
                    currentSquare = LeastF(_openList);
                else
                    currentSquare = _openList[0];

                _closedList.Add(currentSquare);
                _openList.Remove(currentSquare);

                if(_closedList.Exists(x => x.Location == TargetSquare))
                {
                    _path = ShortestPath();
                    return _path; //Path is Found
                }

                stepFromStart++; 

                List<SquareNode> adjacentSquares = Walkable(currentSquare);
                
                //Choose the lowest F Score Square 
                foreach (SquareNode sn in adjacentSquares)
                {
                        int indexOpened = _openList.FindIndex(match => match.Identification == sn.Identification);

                        if(indexOpened == -1) //Not in the open list
                        {
                            _openList.Add(sn);
                        }
                        else // Already in the Open List - Check F Score
                        {

                        int _distance = 
                            ManhattanDistance(GetGridReference(currentSquare.ParentIdentification), sn.Location);

                        int tempG = currentSquare.G + _distance;

                            if(tempG < sn.G)
                            {
                                SquareNode cloneSquareNode = sn;
                                cloneSquareNode.ParentIdentification = currentSquare.Identification;
                                _openList[_openList.FindIndex(match => match.Identification == sn.Identification)] = cloneSquareNode;
                            }
                        }
                }
                
            } while (_openList.Count != 0); // No Path was found;

            return null;
        }

        //Least F Score 
        private SquareNode LeastF(List<SquareNode> squares)
        {
            SquareNode lowestFScore = squares[0];

            for(int c=0; c<squares.Count-1; c++)
            {
                if (lowestFScore.F > squares[c].F)
                    lowestFScore = squares[c];
            }

            return lowestFScore;
        }

        //Grid reference as Vector
        public Vector2 GridReference(Vector2 screenPosition)
        {
            int _x=0, _y=0;

            if (this.Level != null && this.Level.TileWidth > 0 && this.Level.TileHeight >0)
            {
                _x = (int)screenPosition.X / this.Level.TileWidth;
                _y = (int)screenPosition.Y / this.Level.TileHeight;
            }

            return new Vector2(_x, _y);
        }

        //Grid reference from X & Y
        public void GridReference(int x, int y, out Vector2 gridReference)
        {
            gridReference = GridReference(new Vector2(x, y));
        }

        //Get ID from Grid reference 
        public int GetGridID(Vector2 gridReference)
        {
            return ((int)gridReference.X + (int)gridReference.Y * _columns);
        }

        //Get Grid Reference by ID 
        public Vector2 GetGridReference(int GridID)
        {
            if (_columns > 0)
            {
                int y = GridID / _columns;
                int x = GridID % _columns;

                return new Vector2(x, y);
            }

            return Vector2.Zero;
        }

        //Get Screen Location from Grid Reference 
        public Vector2 GetScreenLocation (Vector2 gridReference)
        {
            int _x=0, _y=0;

            if (this.Level != null && this.Level.TileWidth > 0 && this.Level.TileHeight > 0)
            {
                _x = (int) gridReference.X * this.Level.TileWidth;
                _y = (int) gridReference.Y * this.Level.TileHeight;
            }

            return new Vector2(_x, _y);
        }

        //Calculate the random number of blocks to target location
        public int ManhattanDistance(Vector2 Start, Vector2 End)
        {
            return (int)(Math.Abs(Start.X - End.X) + Math.Abs(Start.Y - End.Y));
        }

        //Screen reference as a VECTOR2
        public SquareNode GridScore(SquareNode currentSquare)
        {
            Vector2 _current = currentSquare.Location;
            Vector2 _end = TargetSquare;

            currentSquare.H = ManhattanDistance(_current, _end);
            currentSquare.G = stepFromStart;
            currentSquare.F = currentSquare.H + currentSquare.G;

            return currentSquare;
        }

        //Makes a list of Walkable Grid Squares
        private List<SquareNode> Walkable(SquareNode currentSquare)
        {
            List<SquareNode> AdjacentSquares = new List<SquareNode>();

            SquareNode adjacentSquare;

            int UpId = currentSquare.Identification - _columns;
            int RightId = currentSquare.Identification + 1;
            int DownId = currentSquare.Identification + _columns;
            int LeftId = currentSquare.Identification - 1;
            
            if ((GetGridReference(UpId).X == currentSquare.Location.X && 
                GetGridReference(UpId).Y < currentSquare.Location.Y) && IsFree(GetGridReference(UpId))) 
            {
                adjacentSquare = new SquareNode()
                {
                    Identification = UpId,
                    ParentIdentification = currentSquare.Identification,
                    Location = GetGridReference(UpId)
                };

                adjacentSquare = GridScore(adjacentSquare);

                int _index = _closedList.FindIndex(match => match.Identification == adjacentSquare.Identification);

                if ( _index == -1)
                    AdjacentSquares.Add(adjacentSquare);
            }
            if ((GetGridReference(RightId).Y == currentSquare.Location.Y
                && GetGridReference(RightId).X > currentSquare.Location.X) && IsFree(GetGridReference(RightId)))
            {
                adjacentSquare = new SquareNode()
                {
                    Identification = RightId,
                    ParentIdentification = currentSquare.Identification,
                    Location = GetGridReference(RightId)
                };

                adjacentSquare = GridScore(adjacentSquare);

                int _index = _closedList.FindIndex(match => match.Identification == adjacentSquare.Identification);

                if (_index == -1)
                    AdjacentSquares.Add(adjacentSquare);
            }
            if ((GetGridReference(DownId).X == currentSquare.Location.X
                && GetGridReference(DownId).Y > currentSquare.Location.Y) && IsFree(GetGridReference(DownId)))
            {
                adjacentSquare = new SquareNode()
                {
                    Identification = DownId,
                    ParentIdentification = currentSquare.Identification,
                    Location = GetGridReference(DownId)
                };
                adjacentSquare = GridScore(adjacentSquare);

                int _index = _closedList.FindIndex(match => match.Identification == adjacentSquare.Identification);

                if (_index == -1)
                    AdjacentSquares.Add(adjacentSquare);
            }
            if ((GetGridReference(LeftId).Y == currentSquare.Location.Y
                && GetGridReference(LeftId).X < currentSquare.Location.X) && IsFree(GetGridReference(LeftId)))
            {
                adjacentSquare = new SquareNode()
                {
                    Identification = LeftId,
                    ParentIdentification = currentSquare.Identification,
                    Location = GetGridReference(LeftId)
                };
                adjacentSquare = GridScore(adjacentSquare);

                int _index = _closedList.FindIndex(match => match.Identification == adjacentSquare.Identification);

                if (_index == -1)
                    AdjacentSquares.Add(adjacentSquare);
            }

            return AdjacentSquares;
        }

        //Check if a grid reference is free and not blocked
        private bool IsFree(Vector2 gridReference)
        {
            int index = GetGridID(gridReference);
            if(this.Level != null && this.Level.Tiles.Count > 0 && index >=0)
            if (this.Level.Tiles[index].AssetName == "_Blank_")
                return true;

            return false;
        }

        public void Step(GameTime gameTime)
        {
            float _delta = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_index == 0) Target = GetScreenLocation(_path[0].Location);

            Vector2 step =
                StepTowards.Step(_delta,
                _parent.Position, Target);

            _parent.Position +=
               new Vector2((int)step.X, (int)step.Y);

            //If Entity reach Target then look for Next Target 

            if (Math.Ceiling(_parent.X + (int)_delta) >= Target.X &&
                Math.Floor(_parent.X - (int)_delta) <= Target.X)
            {
                if (Math.Ceiling(_parent.Y + _delta) >= Target.Y &&
                    Math.Floor(_parent.Y - _delta) <= Target.Y)
                {
                    NextStep(gameTime);
                }
            }

            // --------------------------------------------------    
        }

        void NextStep(GameTime gameTime)
        {

            //If moving forward through array
            if (_index < this._path.Count - 1) _index++;

            Target = GetScreenLocation( this._path[_index].Location);
        }

        //Find the shortest Path 
        private List<SquareNode> ShortestPath ()
        {
            List<SquareNode> _temp = new List<SquareNode>();
            int nextParent = _closedList[_closedList.Count-1].Identification;

            do
            {
                nextParent = _closedList.FindIndex(match => match.Identification == nextParent);
                _temp.Add(_closedList[nextParent]);
                nextParent = _closedList[nextParent].ParentIdentification;
            } while (!_temp.Exists(match => match.Location == _closedList[0].Location));

            _temp.Reverse();
            return _temp;
        }
    }
    
    public static class StepTowards
    {
        public static Vector2 Step(float delta, Vector2 current, Vector2 target)
        {
            float x = target.X - current.X; if (x > -1 && x < 0) x = 0;
            float y = target.Y - current.Y; if (y > -1 && y < 0) y = 0;

            if (x == 0 && y == 0) return Vector2.Zero; //Reached Target

            //Move Towards the Target in vector Direction
            if (x < 0) x = -1;
            if (x > 0) x = 1;
            if (y < 0) y = -1;
            if (y > 0) y = 1;

            x = x * delta;
            y = y * delta;

            return new Vector2(x, y);
        }
    }
}

namespace Elevator.Model
{
    enum ElevatorMoveDirection
    {
        Undefined,
        Up,
        Down
    }

    class Elevator
    {
        public int CurrentFloor { get; set; }
        public bool[] InnerActiveFloorButtons { get; set; }
        public bool[] OuterActiveFloorButtons { get; set; }
        public int CurrentMass { get; set; }
        public readonly int MaximumMass = 400;
        public ElevatorMoveDirection MoveDirection { get; set; }

        public Elevator(int NumberOfFloors)
        {
            this.InnerActiveFloorButtons = new bool[NumberOfFloors];
            this.OuterActiveFloorButtons = new bool[NumberOfFloors];
            this.CurrentFloor = 1;
            this.MoveDirection = ElevatorMoveDirection.Undefined;
        }
    }
}

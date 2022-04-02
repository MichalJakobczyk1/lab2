using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstract_classes
{
    class Program
    {
        public interface IIterator
        {
            public abstract bool HasNext();
            public abstract int GetNext();
        }
        public interface IAggregate
        {
            public abstract IIterator CreateIterator();
            public abstract IIterator CreateReversedIterator();
            public abstract IIterator CreateDividedIterator(int divide);
            public abstract IIterator CreateEvenAndOddIterator();
        }
        public class ArrayIntAggregate : IAggregate
        {
            internal int[] array = { 1, 2, 3, 4, 5 };
            public IIterator CreateIterator()
            {
                return new ArrayIntIterator(this);
            }
            public IIterator CreateReversedIterator()
            {
                return new ReversedIterator(this);
            }
            public IIterator CreateDividedIterator(int divide)
            {
                return new DividedIterator(this, divide);
            }
            public IIterator CreateEvenAndOddIterator()
            {
                return new EvenAndOddIterator(this);
            }
        }
        public class EvenAndOddIterator : IIterator
        {
            private int index = 0;
            private ArrayIntAggregate aggregate;
            public EvenAndOddIterator(ArrayIntAggregate Aggregate)
            {
                aggregate = Aggregate;
                var even = Array.FindAll(aggregate.array, (e) => e % 2 == 0);
                var odd = Array.FindAll(aggregate.array, (e) => e % 2 != 0);
                Array.Sort(even, odd);
                Array.Reverse(odd);

                aggregate.array = even.Concat(odd).ToArray();
            }

            public int GetNext()
            {
                return aggregate.array[index++];
            }
            public bool HasNext()
            {
                return index < aggregate.array.Length;
            }
        }
        public sealed class DividedIterator : IIterator
        {
            private int index = 0;
            private int divide;
            private ArrayIntAggregate aggregate;
            public DividedIterator(ArrayIntAggregate Aggregate, int Divide)
            {
                aggregate = Aggregate;
                divide = Divide;
            }
            public int GetNext()
            {
                while (index < aggregate.array.Length && aggregate.array[index] % divide == 0)
                {
                    index++;
                }

                return aggregate.array[index++];
            }
            public bool HasNext()
            {
                return index < aggregate.array.Length;
            }
        }
        public sealed class ArrayIntIterator : IIterator
        {
            private int index = 0;
            private ArrayIntAggregate aggregate;
            public ArrayIntIterator(ArrayIntAggregate Aggregate)
            {
                aggregate = Aggregate;
            }
            public int GetNext()
            {
                return aggregate.array[index++];
            }
            public bool HasNext()
            {
                return index < aggregate.array.Length;
            }
        }
        public sealed class ReversedIterator : IIterator
        {
            private int index = 0;
            private ArrayIntAggregate aggregate;

            public ReversedIterator(ArrayIntAggregate Aggregate)
            {
                aggregate = Aggregate;
                index = aggregate.array.Length - 1;
            }
            public int GetNext()
            {
                return aggregate.array[index--];
            }
            public bool HasNext()
            {
                return index >= 0;
            }

        }
        public interface IMatrixAggregate
        {
            public abstract IIterator CreateMatrixIterator();
        }
        public interface IDriveable
        {
            int Drive(int distance);
        }
        public interface IFly
        {
            public void Fly(int distance);
        }
        public interface IFlyable
        {
            void TakeOff();
            int Fly(int distance);
            void Land();
        }
        public interface ISwimmingable
        {
            int Swim(int distance);
        }
        public class Duck : IFlyable, ISwimmingable
        {
            public int Fly(int distance)
            {
                Console.WriteLine("Duck is flying");
                return -1;
            }

            public void Land()
            {
                Console.WriteLine("Duck is landing");
            }

            public int Swim(int distance)
            {
                Console.WriteLine("Duck is swimming");
                return -1;
            }

            public void TakeOff()
            {
                Console.WriteLine("Duck is taking off");
            }
        }
        public class Wasp : IFlyable
        {
            public int Fly(int distance)
            {
                Console.WriteLine("Wasp is flying");
                return -1;
            }
            public void Land()
            {
                Console.WriteLine("Wasp is landing");
            }
            public void TakeOff()
            {
                Console.WriteLine("Wasp is taking off");
            }
        }
        public class Bicycle : Vehicle
        {
            public bool isDriver { get; set; }
            public override decimal Drive(int distance)
            {
                if (isDriver)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public override string ToString()
            {
                return $"Bicycle{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
            }
        }
        public class Car : Vehicle
        {
            public bool isFuel { get; set; }
            public bool isEngineWorking { get; set; }
            public override decimal Drive(int distance)
            {
                if (isFuel && isEngineWorking)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public override string ToString()
            {
                return $"Car{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}";
            }
        }
        public class ElectricScooter : Scooter
        {
            public int BatteriesLevel
            {
                get
                {
                    return BatteriesLevel;
                }
                set
                {
                    if (value > 100 || value < 0)
                    {
                        throw new ArgumentException("Batteries must be between 0% and 100%");
                    }
                }
            }
            public int MaxRange { get; init; }
            public bool ChargeBatteries()
            {
                return BatteriesLevel == 100;
            }
            public override decimal Drive(int distance)
            {
                if (isDriver && BatteriesLevel > 0 && distance <= MaxRange)
                {
                    _mileage += distance;
                    BatteriesLevel -= distance / MaxRange;
                    return (decimal)(distance / (double)MaxSpeed) + BatteriesLevel;
                }
                return -1;
            }
            public override string ToString()
            {
                return $"ElectricScooter{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}, MaxRange: {MaxRange}, BatteriesLevel: {BatteriesLevel}}}"; ;
            }
        }
        public class HorseCart : Vehicle
        {
            public bool IsHorse { get; set; }
            public override decimal Drive(int distance)
            {
                if (IsHorse)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }

            public override string ToString()
            {
                return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
            }
        }
        public class Hydroplane : Vehicle, IFlyable, ISwimmingable
        {
            public bool isPilot { get; set; }
            public override decimal Drive(int distance)
            {
                if (isPilot)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }

            public int Fly(int distance)
            {
                Console.WriteLine("Hydroplane is flying");
                return -1;
            }
            public void Land()
            {
                Console.WriteLine("Hydroplane is landing");
            }

            public int Swim(int distance)
            {
                Console.WriteLine("Hydroplane is swimming");
                return -1;
            }

            public void TakeOff()
            {
                Console.WriteLine("Hydroplane is taking off");
            }
        }
        public class KickScooter : Scooter
        {
            public override decimal Drive(int distance)
            {
                if (isDriver)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public override string ToString()
            {
                return $"KickScooter{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
            }
        }
        public class Plane : Vehicle, IFlyable
        {
            public bool isPilot { get; set; }
            public override decimal Drive(int distance)
            {
                if (isPilot)
                {
                    _mileage += distance;
                    return (decimal)(distance / (double)MaxSpeed);
                }
                return -1;
            }
            public int Fly(int distance)
            {
                throw new NotImplementedException();
            }

            public void Land()
            {
                throw new NotImplementedException();
            }

            public void TakeOff()
            {
                throw new NotImplementedException();
            }
        }
        public abstract class Scooter : Vehicle
        {
            public bool isDriver { get; set; }
        }
        public abstract class Vehicle
        {
            public double Weight { get; init; }
            public int MaxSpeed { get; init; }
            protected int _mileage;
            public int Mealeage
            {
                get { return _mileage; }
            }
            public abstract decimal Drive(int distance);
            public override string ToString()
            {
                return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
            }
        }
        static void Main(string[] args)
        {
            Vehicle horseCart = new HorseCart() { MaxSpeed = 20, IsHorse = true, Weight = 200 };

            /// init sprawia że property jest tylko do odczytu i nie można jej zmieniać po deklaracji

            Console.WriteLine(horseCart);
            horseCart.Drive(100);
            Console.WriteLine(horseCart);
            horseCart.Drive(100);
            Console.WriteLine(horseCart);

            Console.WriteLine();

            Vehicle[] vehicles = new Vehicle[5];
            vehicles[0] = new Bicycle() { Weight = 15, MaxSpeed = 25, isDriver = true };
            vehicles[1] = new Bicycle() { Weight = 10, MaxSpeed = 30, isDriver = false };
            vehicles[2] = new HorseCart() { Weight = 300, MaxSpeed = 20, IsHorse = true };
            vehicles[3] = new Car() { Weight = 800, MaxSpeed = 120, isEngineWorking = true, isFuel = true };
            vehicles[4] = new Car() { Weight = 1500, MaxSpeed = 260, isEngineWorking = true, isFuel = true };

            int bicycleCounter = 0;

            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] is Bicycle)
                {
                    Bicycle bicycle = (Bicycle)vehicles[i];
                    Console.WriteLine($"Is there a bicycle driver: {bicycle.isDriver}");
                    bicycleCounter++;
                }
            }

            Console.WriteLine($"There are {bicycleCounter} bicycles");

            Console.WriteLine();

            IFlyable flyingObject = new Duck();
            ISwimmingable swimDuck = new Duck();
            flyingObject = new Plane();

            int swimmingFlyingCounter = 0;

            IFlyable[] flyables = new IFlyable[6];
            flyables[0] = new Duck();
            flyables[1] = new Duck();
            flyables[2] = new Wasp();
            flyables[3] = new Hydroplane() { Weight = 15, MaxSpeed = 25, isPilot = true };
            flyables[4] = new Plane() { Weight = 15, MaxSpeed = 25, isPilot = true };
            flyables[5] = new Duck();

            foreach (var item in flyables)
            {
                if (item is ISwimmingable && item is IFlyable)
                {
                    swimmingFlyingCounter++;
                }
            }
            Console.WriteLine($"There are: {swimmingFlyingCounter} objects that can fly and swim\n");

            //przykład wykorzystania iteratora
            IAggregate tab = new ArrayIntAggregate();
            IIterator iterator = tab.CreateIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
        }
    }
}
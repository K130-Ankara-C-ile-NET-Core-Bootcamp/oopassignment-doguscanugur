
using OOPAssignment.Interfaces;
using OOPAssignment.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment.Classes
{
    public class Surface : ISurface, ICollidableSurface, Interfaces.IObserver<CarInfo>
    {
        long _width, _height;
        private readonly List<CarInfo> ObservableCars = new List<CarInfo>();

        public Surface(long width, long height)
        {
            _width = width;
            _height = height;
        }

        public Surface()
        {

        }

        public long Width => _width;
        public long Height => _height;

        public List<CarInfo> GetObservables()
        {
            List<CarInfo> cars = new List<CarInfo>();
            if (ObservableCars != null)
            {
                foreach (var item in ObservableCars)
                {
                    cars.Add(item);
                }
            }
            return cars;

        }

        public bool IsCoordinatesEmpty(Coordinates coordinates)
        {

            if (ObservableCars != null)
            {
                foreach (var item in ObservableCars)
                {
                    if (item.Cordinates.X == coordinates.X & item.Cordinates.Y == coordinates.Y)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsCoordinatesInBounds(Coordinates coordinates)
        {
            if (coordinates.X < 0 || coordinates.Y < 0 || coordinates.X > Width || coordinates.Y > Height)
            {
                return false;
            }
            return true;

        }

        public void Update(CarInfo provider)
        {


            var carlist = GetObservables();
            if (carlist.Contains(provider))
            {
                var car = ObservableCars.FirstOrDefault(x => x.CarId == provider.CarId);
                car.Cordinates = provider.Cordinates;
            }

            else if (IsCoordinatesEmpty(provider.Cordinates))
                ObservableCars.Add(provider);
            else
            {
                var car = ObservableCars.FirstOrDefault(x => x.CarId == provider.CarId);
                if (car.Cordinates.X == provider.Cordinates.X && car.Cordinates.Y == provider.Cordinates.Y)
                    throw new Exception();
                else
                    ObservableCars.Add(provider);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.Business.Managers;
using WpfAppCourse.Domain.Entities;

namespace WpfAppCourse.Business.Infrastructure
{
    public static class DbTestData
    {
        public static void SetupData(CarManager carManager)
        {
            // Добавление автомобилей
            carManager.AddRange(new List<Car>
            {
                new Car
                {
                    CarNumber = 5445,
                    CarWeight = 25,
                    CarName = "Scania"
                },
                new Car
                {
                    CarNumber = 6732,
                    CarWeight = 20,
                    CarName = "Volvo"
                },
                new Car
                {
                    CarNumber = 3609,
                    CarWeight = 20,
                    CarName = "DAF"
                },
                new Car
                {
                    CarNumber = 7835,
                    CarWeight = 25,
                    CarName = "Mercedes"
                },
                new Car
                {
                    CarNumber = 8864,
                    CarWeight = 20,
                    CarName = "Tatra"
                }
            });

            var cars = carManager.Cars.ToArray();
            // Добавление заявок
            // Для первой машины
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 5,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Плитка",
                    Destination = "Гродно"
                },
            cars[0].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 6,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Кровля",
                    Destination = "Гродно"
                },
            cars[0].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 8,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Гипсокартон",
                    Destination = "Гродно"
                },
            cars[0].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 4,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Саморезы",
                    Destination = "Гродно"
                },
            cars[0].CarId);
            //Для второй машины
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 3,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Мебель",
                    Destination = "Витебск"
                },
            cars[1].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 7,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Вагонка",
                    Destination = "Витебск"
                },
            cars[1].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 6,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Бумага",
                    Destination = "Витебск"
                },
            cars[1].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 4,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Ламинат",
                    Destination = "Витебск"
                },
            cars[1].CarId);
            //Для третьей машины
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 7,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "ПВХ панели",
                    Destination = "Орша"
                },
            cars[2].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 4,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "Доска обрезная",
                    Destination = "Орша"
                },
            cars[2].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 6,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "Обои",
                    Destination = "Орша"
                },
            cars[2].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 2,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "Картон",
                    Destination = "Орша"
                },
            cars[2].CarId);
        }
    }
}


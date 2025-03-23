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
                },
                new Car
                {
                    CarNumber = 8344,
                    CarWeight = 25,
                    CarName = "Renault"
                },
                new Car
                {
                    CarNumber = 2254,
                    CarWeight = 20,
                    CarName = "Volvo"
                },
                new Car
                {
                    CarNumber = 2757,
                    CarWeight = 20,
                    CarName = "Scania"
                },
                new Car
                {
                    CarNumber = 4657,
                    CarWeight = 20,
                    CarName = "Scania"
                },
                new Car
                {
                    CarNumber = 3247,
                    CarWeight = 20,
                    CarName = "Volvo"
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
                    //ClientId = 1,
                },
            cars[0].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 6,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Кровля",
                    Destination = "Гродно"
                    //ClientId = 1,
                },
            cars[0].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 8,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Гипсокартон",
                    Destination = "Гродно"
                    //ClientId = 1,
                },
            cars[0].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 4,
                    DateOfDispatch = new DateTime(2025, 01, 23),
                    CargoName = "Саморезы",
                    Destination = "Гродно"
                    //ClientId = 1,
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
                    //ClientId = 1,
                },
            cars[1].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 7,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Вагонка",
                    Destination = "Витебск"
                    //ClientId = 1,
                },
            cars[1].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 6,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Бумага",
                    Destination = "Витебск"
                    //ClientId = 1,
                },
            cars[1].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 4,
                    DateOfDispatch = new DateTime(2025, 01, 10),
                    CargoName = "Ламинат",
                    Destination = "Витебск"
                    //ClientId = 1,
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
                    //ClientId = 1,
                },
            cars[2].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 4,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "Доска обрезная",
                    Destination = "Орша"
                    //ClientId = 1,
                },
            cars[2].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 6,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "Обои",
                    Destination = "Орша"
                    //ClientId = 1,
                },
            cars[2].CarId);
            carManager.AddApplicationToCar(
                new Application
                {
                    CargoWeight = 2,
                    DateOfDispatch = new DateTime(2025, 01, 15),
                    CargoName = "Картон",
                    Destination = "Орша"
                    //ClientId = 1,
                },
            cars[2].CarId);

            // Добавление водителей
            // Для первой машины
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Максим",
                    DriverSurname = "Петров",
                    DriverAge = 28,
                    DriverExperience = 4,
                    DateOfAdmission = new DateTime(2024, 05, 23)
                },
            cars[0].CarId);
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Сергей",
                    DriverSurname = "Ивашенко",
                    DriverAge = 32,
                    DriverExperience = 10,
                    DateOfAdmission = new DateTime(2022, 07, 19)
                },
            cars[0].CarId);
            // Для второй машины
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Аркадий",
                    DriverSurname = "Демченко",
                    DriverAge = 31,
                    DriverExperience = 8,
                    DateOfAdmission = new DateTime(2023, 04, 03)
                },
            cars[1].CarId);
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Олег",
                    DriverSurname = "Крупицкий",
                    DriverAge = 26,
                    DriverExperience = 5,
                    DateOfAdmission = new DateTime(2023, 04, 14)
                },
            cars[1].CarId);
            // Для третьей машины
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Игорь",
                    DriverSurname = "Сушко",
                    DriverAge = 29,
                    DriverExperience = 7,
                    DateOfAdmission = new DateTime(2021, 03, 06)
                },
            cars[2].CarId);
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Алексей",
                    DriverSurname = "Федунков",
                    DriverAge = 29,
                    DriverExperience = 9,
                    DateOfAdmission = new DateTime(2024, 02, 10)
                },
            cars[2].CarId);
            // Для четвертой машины
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Артемий",
                    DriverSurname = "Семак",
                    DriverAge = 30,
                    DriverExperience = 6,
                    DateOfAdmission = new DateTime(2022, 08, 16)
                },
            cars[3].CarId);
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Александр",
                    DriverSurname = "Бородач",
                    DriverAge = 25,
                    DriverExperience = 7,
                    DateOfAdmission = new DateTime(2023, 01, 19)
                },
            cars[3].CarId);
            // Для пятой машины
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Артур",
                    DriverSurname = "Панчак",
                    DriverAge = 32,
                    DriverExperience = 8,
                    DateOfAdmission = new DateTime(2021, 09, 12)
                },
            cars[4].CarId);
            carManager.AddDriverToCar(
                new Driver
                {
                    DriverName = "Александр",
                    DriverSurname = "Радионов",
                    DriverAge = 29,
                    DriverExperience = 7,
                    DateOfAdmission = new DateTime(2023, 05, 29)
                },
            cars[4].CarId);

        }
    }
}


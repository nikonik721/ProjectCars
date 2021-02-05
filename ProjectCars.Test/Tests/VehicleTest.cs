using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectCars.BL.Interface;
using ProjectCars.BL.Services;
using ProjectCars.Controllers;
using ProjectCars.DL.Interface;
using ProjectCars.Extensions;
using ProjectCars.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjectCars.Test
{
    public class VehicleTests
	{
		private IMapper _mapper;
		private Mock<IVehicleRepository> _vehicleRepository;
		private IVehicleService _vehicleService;
		private VehicleController _controller;

		IList<Vehicle> _vehicle = new List<Vehicle>()
			{
				{ new Vehicle {

				VehicleId = 8,
				VehicleBrand = "Mercedes-Benz",
				VehicleModel = "CLA 220",
				DateOfManufacturing = new DateTime(2017, 02, 03),
				VehicleColor = "Dark Gray",
				VehicleType = "AWD",
				Engine = "2.2 AMG, 177HP",
				Fuel = "Diesel"

				} },

				{ new Vehicle {

				VehicleId = 10,
				VehicleBrand = "Subaru",
				VehicleModel = "Impreza",
				DateOfManufacturing = new DateTime(2019, 02, 03),
				VehicleColor = "Light Grey Metallic",
				VehicleType = "AWD",
				Engine = "2.0, 152HP",
				Fuel = "Gasoline"

				} },
			};

		public VehicleTests()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapping());
			});

			_mapper = mockMapper.CreateMapper();

			_vehicleRepository = new Mock<IVehicleRepository>();


			_vehicleService = new VehicleService(_vehicleRepository.Object);

			//inject
			_controller = new VehicleController(_vehicleService, _mapper);
		}

		[Fact]
		public async Task Vehicle_GetAll_Count_Check()
		{
			//setup
			var expectedCount = 2;

			_vehicleRepository.Setup(x => x.GetAll())
				.ReturnsAsync(_vehicle);

			//Act
			var result = await _controller.GetAll();

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var positions = okObjectResult.Value as IEnumerable<VehicleResponse>;
			Assert.NotNull(positions);
			Assert.Equal(expectedCount, positions.Count());
		}

		[Fact]
		public async Task Vehicle_Update_VehicleColor()
		{
			//setup
			var VehicleId = 8;
			var VehicleColor = "Light Blue";

			var position = _vehicle.FirstOrDefault(x => x.VehicleId == VehicleId);
			position.VehicleColor = VehicleColor;


			_vehicleRepository.Setup(x => x.Update(It.IsAny<Vehicle>())).Callback(() =>
			{
				position.VehicleColor = VehicleColor;
			}).Returns(() => Task<Vehicle>.Factory.StartNew(() => new Vehicle()
			{
				VehicleId = position.VehicleId,
				VehicleBrand = position.VehicleBrand,
				VehicleModel = position.VehicleModel,
				DateOfManufacturing = position.DateOfManufacturing,
				VehicleColor = position.VehicleColor,
				VehicleType = position.VehicleType,
				Engine = position.Engine,
				Fuel = position.Fuel,
			}));

			//Act
			var result = await _controller.Update(_mapper.Map<VehicleRequest>(position));

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var pos = okObjectResult.Value as VehicleResponse;
			Assert.NotNull(pos);
			Assert.Equal(VehicleColor, pos.VehicleColor);
		}

		[Fact]
		public async Task Vehicle_Delete_Existing_VehicleId()
		{
			//setup
			var VehicleId = 10;

			var position = _vehicle.FirstOrDefault(x => x.VehicleId == VehicleId);


			_vehicleRepository.Setup(x => x.GetById(VehicleId)).ReturnsAsync(_vehicle.FirstOrDefault(x => x.VehicleId == VehicleId));
			_vehicleRepository.Setup(x => x.Delete(VehicleId)).Callback(() => _vehicle.Remove(position));

			//Act
			var result = await _controller.Delete(VehicleId);

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.Null(okObjectResult);

			Assert.Null(_vehicle.FirstOrDefault(x => x.VehicleId == VehicleId));
		}

		[Fact]
		public async Task Vehicle_Delete_NotExisting_VehicleId()
		{
			//setup
			var VehicleId = 3;

			var position = _vehicle.FirstOrDefault(x => x.VehicleId == VehicleId);


			_vehicleRepository.Setup(x => x.Delete(VehicleId)).Callback(() => _vehicle.Remove(position));

			//Act
			var result = await _controller.Delete(VehicleId);

			//Assert
			var notFoundObjectResult = result as NotFoundObjectResult;
			Assert.Null(notFoundObjectResult);

			Assert.Null(_vehicle.FirstOrDefault(x => x.VehicleId == VehicleId));
		}

		[Fact]
		public async Task Vehicle_Create_VehicleId()
		{
			//setup
			var position = new Vehicle()
			{
				VehicleId = 2,
				VehicleBrand = "VW",
				VehicleModel = "Jetta",
				DateOfManufacturing = new DateTime(2017, 02, 03),
				VehicleColor = "Burgundy Metallic",
				VehicleType = "FWD",
				Engine = "1.4 TSI, 150HP",
				Fuel = "Gasoline"

			};

			_vehicleRepository.Setup(x => x.Create(It.IsAny<Vehicle>())).Callback(() =>
			{
				_vehicle.Add(position);
			}).Returns(() => Task<Vehicle>.Factory.StartNew(() => new Vehicle()
			{
				VehicleId = 2,
				VehicleBrand = "Porsche",
				VehicleModel = "Panamera",
				DateOfManufacturing = new DateTime(2017, 02, 03),
				VehicleColor = "Blue Metallic",
				VehicleType = "AWD",
				Engine = "Turbo 4.0, 549HP",
				Fuel = "Gasoline"

			}));

			//Act
			var result = await _controller.Create(_mapper.Map<VehicleRequest>(position));

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var pos = okObjectResult.Value as VehicleResponse;
			Assert.NotNull(pos);

			Assert.NotNull(_vehicle.FirstOrDefault(x => x.VehicleId == position.VehicleId));
		}
	}
}

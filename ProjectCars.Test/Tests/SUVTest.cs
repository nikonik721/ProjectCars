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
	public class SUVTests
	{
		private IMapper _mapper;
		private Mock<ISUVRepository> _suvRepository;
		private ISUVService _suvService;
		private SUVController _controller;

		IList<SUV> _suv = new List<SUV>()
			{
				{ new SUV {

				SUVId = 15,
				SUVBrand = "Skoda",
				SUVModel = "Kodiaq SE L",
				DateOfManufacturing = new DateTime(2020, 05, 30),
				SUVType = "FWD",
				Engine = "1.5 TSI DSG, 150HP",
				Fuel = "Diesel/ Gasoline"

				} },

				{ new SUV {

				SUVId = 13,
				SUVBrand = "BMW",
				SUVModel = "X3 M Sport",
				DateOfManufacturing = new DateTime(2020, 05, 30),
				SUVType = "AWD",
				Engine = "360HP",
				Fuel = "Gasoline"

				} },
			};

		public SUVTests()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapping());
			});

			_mapper = mockMapper.CreateMapper();

			_suvRepository = new Mock<ISUVRepository>();


			_suvService = new SUVService(_suvRepository.Object);

			//inject
			_controller = new SUVController(_suvService, _mapper);
		}

		[Fact]
		public async Task SUV_GetAll_Count_Check()
		{
			//setup
			var expectedCount = 2;

			_suvRepository.Setup(x => x.GetAll())
				.ReturnsAsync(_suv);

			//Act
			var result = await _controller.GetAll();

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var positions = okObjectResult.Value as IEnumerable<SUVResponse>;
			Assert.NotNull(positions);
			Assert.Equal(expectedCount, positions.Count());
		}

		[Fact]
		public async Task SUV_Update_SUVFuel()
		{
			//setup
			var SUVId = 13;
			var SUVFuel = "Volkswagen";

			var position = _suv.FirstOrDefault(x => x.SUVId == SUVId);
			position.Fuel = SUVFuel;


			_suvRepository.Setup(x => x.Update(It.IsAny<SUV>())).Callback(() =>
			{
				position.Fuel = SUVFuel;
			}).Returns(() => Task<SUV>.Factory.StartNew(() => new SUV()
			{
				SUVId = position.SUVId,
				SUVBrand = position.SUVBrand,
				SUVModel = position.SUVModel,
				DateOfManufacturing = position.DateOfManufacturing,
				SUVType = position.SUVType,
				Engine = position.Engine,
				Fuel = position.Fuel

			}));

			//Act
			var result = await _controller.Update(_mapper.Map<SUVRequest>(position));

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var pos = okObjectResult.Value as SUVResponse;
			Assert.NotNull(pos);
			Assert.Equal(SUVFuel, pos.Fuel);
		}

		[Fact]
		public async Task SUV_Delete_Existing_SUVId()
		{
			//setup
			var SUVId = 15;

			var position = _suv.FirstOrDefault(x => x.SUVId == SUVId);


			_suvRepository.Setup(x => x.GetById(SUVId)).ReturnsAsync(_suv.FirstOrDefault(x => x.SUVId == SUVId));
			_suvRepository.Setup(x => x.Delete(SUVId)).Callback(() => _suv.Remove(position));

			//Act
			var result = await _controller.Delete(SUVId);

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.Null(okObjectResult);

			Assert.Null(_suv.FirstOrDefault(x => x.SUVId == SUVId));
		}

		[Fact]
		public async Task SUV_Delete_NotExisting_SUVId()
		{
			//setup
			var SUVId = 3;

			var position = _suv.FirstOrDefault(x => x.SUVId == SUVId);


			_suvRepository.Setup(x => x.Delete(SUVId)).Callback(() => _suv.Remove(position));

			//Act
			var result = await _controller.Delete(SUVId);

			//Assert
			var notFoundObjectResult = result as NotFoundObjectResult;
			Assert.Null(notFoundObjectResult);

			Assert.Null(_suv.FirstOrDefault(x => x.SUVId == SUVId));
		}

		[Fact]
		public async Task SUV_Create_SUVFuel()
		{
			//setup
			var position = new SUV()
			{
				SUVId = 13,
				SUVBrand = "Volkswagen",
				SUVModel = "T-Cross S",
				DateOfManufacturing = new DateTime(2020, 05, 30),
				SUVType = "AWD",
				Engine = "1.6 TSI, 95HP",
				Fuel = "Gasoline"
			};

			_suvRepository.Setup(x => x.Create(It.IsAny<SUV>())).Callback(() =>
			{
				_suv.Add(position);
			}).Returns(() => Task<SUV>.Factory.StartNew(() => new SUV()
			{
				SUVId = 13,
				SUVBrand = "BMW",
				SUVModel = "X5 M Sport",
				DateOfManufacturing = new DateTime(2020, 05, 30),
				SUVType = "AWD",
				Engine = "340HP",
				Fuel = "Gasoline/ Diesel"


			}));

			//Act
			var result = await _controller.Create(_mapper.Map<SUVRequest>(position));

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var pos = okObjectResult.Value as SUVResponse;
			Assert.NotNull(pos);

			Assert.NotNull(_suv.FirstOrDefault(x => x.SUVId == position.SUVId));
		}
	}
}

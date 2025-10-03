using AutoMapper;
using FluentAssertions;
using Ginasio.Api.Controllers;
using Ginasio.Api.Models;
using Ginasio.Core.DTOs;
using Ginasio.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;
using Xunit;

namespace Ginasio.Api.Tests.Controllers
{
    public class GinasioControllerTests
    {
        private readonly Mock<IGinasioHandler> _handlerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GinasioController _controller;

        public GinasioControllerTests()
        {
            _handlerMock = new Mock<IGinasioHandler>();
            _mapperMock = new Mock<IMapper>();
            _controller = new GinasioController(_handlerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CriarGinasioAsync_DeveRetornarOk()
        {
            // Arrange
            var gym = new Gym { Nome = "Academia X" };
            var dto = new GinasioDto { Nome = "Academia X" };
            _mapperMock.Setup(m => m.Map<GinasioDto>(gym)).Returns(dto);

            // Act
            var result = await _controller.CriarGinasioAsync(gym);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(new[] { "Ginasio criado" });
            _handlerMock.Verify(h => h.CadastrarGinasioHandler(dto), Times.Once);
        }

        [Fact]
        public async Task EditarGinasio_DeveRetornarOk()
        {
            // Arrange
            var gym = new Gym { Nome = "Academia Y" };
            var dto = new GinasioDto { Nome = "Academia Y" };
            _mapperMock.Setup(m => m.Map<GinasioDto>(gym)).Returns(dto);

            // Act
            var result = await _controller.EditarGinasio(gym, 1);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(new[] { "Ginasio editado" });
            _handlerMock.Verify(h => h.EditarGinasioAsync(dto, 1), Times.Once);
        }

        [Fact]
        public async Task DeletarGinasio_DeveRetornarOk()
        {
            // Act
            var result = await _controller.DeletarGinasio(1);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(new[] { "Ginasio deletado" });
            _handlerMock.Verify(h => h.DeletaGinasioAsync(1), Times.Once);
        }

        [Fact]
        public async Task BuscarGinasios_DeveRetornarOkComDados()
        {
            // Arrange
            var expected = new List<GinasioDto> { new GinasioDto { Nome = "Academia Z" } };
            _handlerMock.Setup(h => h.BuscaGinasioAsync()).ReturnsAsync(expected);

            // Act
            var result = await _controller.BuscarGinasios();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(new[] { expected });
        }

        [Fact]
        public async Task BuscarGinasiosPorNome_DeveRetornarOkComDados()
        {
            // Arrange
            var expected = new GinasioDto { Nome = "Academia A" };
            _handlerMock.Setup(h => h.BuscaGinasioPorNomeAsync("Academia A")).ReturnsAsync(expected);

            // Act
            var result = await _controller.BuscarGinasios("Academia A");

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(new[] { expected });
        }

        [Fact]
        public async Task AdicionaLogoGinasio_DeveRetornarBadRequest_SeArquivoForNulo()
        {
            // Act
            var result = await _controller.AdicionaLogoGinasio(null);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.Value.Should().Be("Nenhum arquivo enviado.");
        }

        [Fact]
        public async Task AdicionaLogoGinasio_DeveRetornarOk_SeUploadValido()
        {
            // Arrange
            var fileBytes = Encoding.UTF8.GetBytes("fake-image");
            var formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "arquivo", "logo.png");
            var dto = new UploadLogoDto { Id = 1, Arquivo = formFile };

            // Act
            var result = await _controller.AdicionaLogoGinasio(dto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var value = okResult!.Value as dynamic;
            ((int)value.Tamanho).Should().Be(fileBytes.Length);
            _handlerMock.Verify(h => h.AdicionaLogoGinasioAsync(dto.Id, It.IsAny<string>()), Times.Once);
        }
    }
}

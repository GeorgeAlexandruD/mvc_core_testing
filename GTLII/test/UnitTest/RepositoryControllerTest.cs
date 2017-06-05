using GTLII.Entities;
using GTLII.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class RepositoryControllerTest
    {
        private IBooksRepository _repo;

        [Theory]
        [InlineData(null)]              // null
        [InlineData("1234512345123")]   // 13 digits
        [InlineData("1234512345")]      // 10 digits
        public void CreateBookRightAmountOfIsbnigits(string value)
        {
            //Arrange
            _repo = new BooksRepository();

            //Act
            bool result = _repo.CreateBook("Some book", value);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("123451234512")]    //12 digits
        [InlineData("12345123451234")]  //14 digits
        [InlineData("123451234")]       //9 digits
        [InlineData("12345123451")]     //11 digits
        public void CreateBookWrongAmountOfIsbnDigits(string value)
        {
            //Arrange
            _repo = new BooksRepository();

            //Act
            bool result = _repo.CreateBook("Some book", value);

            //Assert
            Assert.False(result);
        }
        [Theory]
        [InlineData("This title is 32 characters long")]    //32 Characters
        [InlineData("P")]                                   //1 Character
        public void CreateBookRightAmountOfTitleCharacters(string value)
        {
            //Arrange
            _repo = new BooksRepository();

            //Act
            bool result = _repo.CreateBook(value, "1234512345");

            //Assert
            Assert.True(result);
        }
        [Theory]
        [InlineData("")]                                        //Empty string
        [InlineData("This title is 33 characters longg")]       //33 Characters
        public void CreateBookWrongAmountOfTitleCharacters(string value)
        {
            //Arrange
            _repo = new BooksRepository();

            //Act
            bool result = _repo.CreateBook(value, "1234512345");

            //Assert
            Assert.False(result);
        }
    }
}

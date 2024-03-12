using FakeItEasy;
using FSPBook.Data;
using FSPBook.Pages;
using FSPBook.Portal.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace FSPBook.Test
{
    public class CreatePostTests
    {
        [Fact]
        public void CreatePost_CallsDBContext_AddPost()
        {
            //arrange
            var dbContext = A.Fake<IDbContext>();
            var pageModel = new CreateModel(dbContext)
            {
                ProfileId = 1,
                ContentInput = "content"
            };

            //act
            var result = pageModel.OnPostAsync();

            //assert
            A.CallTo(() => dbContext.AddPost(pageModel.ProfileId, pageModel.ContentInput)).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => dbContext.GetAllProfiles()).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void CreatePost_WithNullProfile_FailsGracefully()
        {
            //arrange
            var dbContext = A.Fake<IDbContext>();
            var pageModel = new CreateModel(dbContext)
            {
                ProfileId = 0,
                ContentInput = null
            };

            //act
            var result = pageModel.OnPostAsync();

            //assert
            Assert.False(pageModel.Success);
            A.CallTo(() => dbContext.GetAllProfiles()).MustHaveHappened(1, Times.Exactly);
        }
    }
}
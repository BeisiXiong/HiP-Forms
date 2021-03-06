﻿// Copyright (C) 2017 History in Paderborn App - Universität Paderborn
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//       http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using PaderbornUniversity.SILab.Hip.Mobile.Shared.BusinessLayer.Models;
using NSubstitute;
using NUnit.Framework;
using PaderbornUniversity.SILab.Hip.Mobile.Shared.Common;
using PaderbornUniversity.SILab.Hip.Mobile.Shared.Common.Contracts;
using PaderbornUniversity.SILab.Hip.Mobile.UI.ViewModels.Views;

namespace PaderbornUniversity.SILab.Hip.Mobile.HipMobileUITests.ViewModels.Views
{
    class ExhibitsOverviewListItemViewModelTest
    {

        [Test, Category("UnitTest")]
        public void Creation_PropertiesFilled()
        {
            var sut = CreateSystemUnderTest();

            Assert.AreEqual ("Exhibit", sut.ExhibitName);
            Assert.AreEqual (42, sut.Distance);
            Assert.NotNull(sut.Image);
        }

        [Test, Category("UnitTest")]
        public void Distance_Formatted()
        {
            var sut = CreateSystemUnderTest();

            Assert.AreEqual("42 m", sut.FormatedDistance);
            sut.Distance = 42000;
            Assert.AreEqual("42 km", sut.FormatedDistance);
        }

        #region HelperMethods

        private ExhibitsOverviewListItemViewModel CreateSystemUnderTest()
        {
            var imageDimensions = Substitute.For<IImageDimension>();
            IoCManager.RegisterInstance(typeof(IImageDimension), imageDimensions);

            var exhibit = Substitute.For<Exhibit>();

            var image = Substitute.For<Image>();
            image.Data = new byte[] { 1, 2, 3, 4 };

            exhibit.Name = "Exhibit";
            exhibit.Image = image;

            return new ExhibitsOverviewListItemViewModel(exhibit, 42);
        }

        #endregion
    }
}

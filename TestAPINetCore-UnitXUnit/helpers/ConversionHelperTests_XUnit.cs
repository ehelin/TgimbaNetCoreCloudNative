using System;
using BLLNetCore.helpers;
using Xunit;
using Shared.interfaces;

namespace TestAPINetCore_Unit.helpers
{
    public class ConversionHelperTests_XUnit : BaseTest
    {
        private IConversion sut = null;

        public ConversionHelperTests_XUnit() {
            sut = new ConversionHelper();
        }

        #region GetSafeBool(val)

        [Fact]
        public void GetSafeBool_ActualTrue()
        {
            object val = true;
            var result = sut.GetSafeBool(val);
            Assert.True(result);
        }

        [Fact]
        public void GetSafeBool_ImpliedTrue()
        {
            object val = 1;
            var result = sut.GetSafeBool(val);
            Assert.True(result);
        }

        [Fact]
        public void GetSafeBool_ActualFalse()
        {
            object val = false;
            var result = sut.GetSafeBool(val);
            Assert.False(result);
        }

        [Fact]
        public void GetSafeBool_ImpliedFalse()
        {
            object val = 0;
            var result = sut.GetSafeBool(val);
            Assert.False(result);
        }

        [Fact]
        public void GetSafeBool_NullFalse()
        {
            object val = null;
            var result = sut.GetSafeBool(val);
            Assert.False(result);
        }

        [Fact]
        public void GetSafeBool_DbNullFalse()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeBool(val);
            Assert.False(result);
        }

        #endregion

        #region GetSafeDecimal(val)

        [Fact]
        public void GetSafeDecimal_HappyPath()
        {
            object val = 1.1;
            var result = sut.GetSafeDecimal(val);
            Assert.True(result == (decimal)1.1);
        }

        [Fact]
        public void GetSafeDecimal_IntToDecimal()
        {
            object val = 1;
            var result = sut.GetSafeDecimal(val);
            Assert.True(result == (decimal)1);
        }

        [Fact]
        public void GetSafeDecimal_Null()
        {
            object val = null;
            var result = sut.GetSafeDecimal(val);
            Assert.True(result == (decimal)0);
        }

        [Fact]
        public void GetSafeDecimal_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeDecimal(val);
            Assert.True(result == (decimal)0);
        }

        #endregion

        #region GetSafeInt(val)

        [Fact]
        public void GetSafeInt_HappyPath()
        {
            object val = 1;
            var result = sut.GetSafeInt(val);
            Assert.True(result == 1);
        }

        [Fact]
        public void GetSafeInt_DecimalToInt()
        {
            object val = 1.1;
            var result = sut.GetSafeInt(val);
            Assert.True(result == 1);
        }
        
        [Fact]
        public void GetSafeInt_Null()
        {
            object val = null;
            var result = sut.GetSafeInt(val);
            Assert.True(result == 0);
        }

        [Fact]
        public void GetSafeInt_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeInt(val);
            Assert.True(result == 0);
        }

        [Fact]
        public void GetSafeInt_EmptyString()
        {
            object val = "";
            var result = sut.GetSafeInt(val);
            Assert.True(result == 0);
        }

        #endregion

        #region GetSafeString(val)

        [Fact]
        public void GetSafeString_HappyIntToString()
        {
            object val = 1;
            var result = sut.GetSafeString(val);
            Assert.True(result == "1");
        }

        [Fact]
        public void GetSafeString_DecimalToString()
        {
            object val = 1.1;
            var result = sut.GetSafeString(val);
            Assert.True(result == "1.1");
        }

        [Fact]
        public void GetSafeString_Null()
        {
            object val = null;
            var result = sut.GetSafeString(val);
            Assert.True(result == String.Empty);
        }

        [Fact]
        public void GetSafeString_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeString(val);
            Assert.True(result == String.Empty);
        }

        #endregion
        
        #region GetSafeDateTime(val)

        [Fact]
        public void GetSafeDateTime_HappyPath()
        {
            object val = "1/2/2020";
            var result = sut.GetSafeDateTime(val);
            Assert.True(result == DateTime.Parse(val.ToString()));
        }

        [Fact]
        public void GetSafeDateTime_Null()
        {
            object val = null;
            var result = sut.GetSafeDateTime(val);
            Assert.True(result == DateTime.MinValue);
        }

        [Fact]
        public void GetSafeDateTime_DbNull()
        {
            object val = DBNull.Value;
            var result = sut.GetSafeDateTime(val);
            Assert.True(result == DateTime.MinValue);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BaseUtility
    {
        /// <summary>
        /// output GUID
        /// </summary>
        /// <returns>GUID</returns>
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
        /// <summary>
        /// output GUID 
        /// </summary>
        /// <returns></returns>
        public static string GetTempGUID()
        {
            var guid = Guid.NewGuid().ToString().ToUpper();
            guid = guid.Substring(8);
            return string.Concat("00000000", guid);
        }
        /// <summary>
        /// Get DateTime from the Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static DateTime GetDBDateTime(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            try
            {
                return Convert.ToDateTime(dbValue);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Get Byte from Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static byte GetDBByte(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return byte.MaxValue;
            }
            try
            {
                return Convert.ToByte(dbValue);
            }
            catch
            {
                return byte.MaxValue;
            }
        }
        /// <summary>
        /// Get Boolen from Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static bool GetDBBoolean(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(dbValue);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Get Int64 from Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static long GetDBInt64(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return 0;
            }
            try
            {
                return Convert.ToInt64(dbValue);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Get Int32 from Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static int GetDBInt32(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(dbValue);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Get Int16 from Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static short GetDBInt16(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return 0;
            }
            try
            {
                return Convert.ToInt16(dbValue);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Get string from Database
        /// </summary>
        /// <param name="dbValue"></param>
        /// <returns></returns>
        public static string GetDBString(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return String.Empty;
            }
            try
            {
                return Convert.ToString(dbValue);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}

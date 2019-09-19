using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.lvrenyang
{
    public class BinarySerialize<T>
    {
        public void Serialize(T obj, string strFilePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(strFilePath, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
            catch (System.Exception ex)
            {
                FileUtils.Log("BinarySerialize-Serialize:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
            }
            finally
            {
                if (null != fs)
                    fs.Close();
            }
        }

        /// <summary>  
        /// Deserialize an instance of T.  
        /// </summary>  
        /// <typeparam name="T">Any type.</typeparam>  
        /// <returns>The result of deserialized.</returns>  
        public T DeSerialize(string strFilePath)
        {
            T t = default(T);
            FileStream fs = null;
            try
            {

                fs = new FileStream(strFilePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                t = (T)formatter.Deserialize(fs);
            }
            catch (System.Exception ex)
            {
                FileUtils.Log("BinarySerialize-DeSerialize:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
            }
            finally
            {
                if (null != fs)
                    fs.Close();
            }
            return t;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Artech.MemLeakByEvents
{
  public  class Encry
    {



        /// <summary>
        /// 加解密演示
        /// </summary>
        public void MainTest()
        {

            try
            {

                //为了实现字节数组到字符窜的转换创建一个UnicodeEncoder

                UnicodeEncoding ByteConverter = new UnicodeEncoding();



                //创建一个字节数组保留原始的，加密的以及解密的数据

                //byte[] dataToEncrypt = ByteConverter.GetBytes("需要加密的数据");



                byte[] dataToEncrypt = ByteConverter.GetBytes("需要加密的数据");

                byte[] encryptedData;

                byte[] decryptedData;



                //创建一个 RSACryptoServiceProvider 的实例来产生公共和私有密钥数据

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();



                //通过这个数据去加密，公共密钥信息
                //使用RSACryptoServiceProvider.ExportParameters(false),
                //和一个 boolean 变量标明是否用 OAEP 填充.

                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);



                MessageBox.Show(Convert.ToBase64String(encryptedData));



                //通过这个数据去解密, 私有密钥使用
                //RSACryptoServiceProvider.ExportParameters(true),
                //和 一个 boolean变量标明是否用 OAEP 填充.

                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);



                //显示解密的信息到控制台. 

                MessageBox.Show(ByteConverter.GetString(decryptedData));

                //Console.Read();

            }

            catch (ArgumentNullException)
            {

                MessageBox.Show("Encryption failed.");

            }

        }


         public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
         {

             try
             {
                  RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                  //输入 RSA Key 信息. 包括 public key 信息.
                  RSA.ImportParameters(RSAKeyInfo);

     
                  //加密这个字节数组和是否用 OAEP 来填充
                  //OAEP 填充仅仅在Microsoft Windows XP 或者
                  //以后的版本中有用

                  return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);

             }
             catch(CryptographicException e)
             {
                  Console.WriteLine(e.Message);
                  return null;
             }

         }

 

          /// <summary>
          /// 不对称解密算法
          /// </summary>
          /// <param name="DataToDecrypt"></param>
          /// <param name="RSAKeyInfo"></param>
          /// <param name="DoOAEPPadding"></param>
          /// <returns></returns>
         public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo,bool DoOAEPPadding)
         {

             try
             {

                  RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                  //输入 RSA Key 信息. 这个需要包括
                  // private key 信息

                  RSA.ImportParameters(RSAKeyInfo);

                  //解密这个字节数组和是否用 OAEP 填充. 

                  return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);

             }
             catch(CryptographicException e)
             {
                  Console.WriteLine(e.ToString());
                  return null;
             }

         }





    }
}
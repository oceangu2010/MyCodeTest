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
        /// �ӽ�����ʾ
        /// </summary>
        public void MainTest()
        {

            try
            {

                //Ϊ��ʵ���ֽ����鵽�ַ��ܵ�ת������һ��UnicodeEncoder

                UnicodeEncoding ByteConverter = new UnicodeEncoding();



                //����һ���ֽ����鱣��ԭʼ�ģ����ܵ��Լ����ܵ�����

                //byte[] dataToEncrypt = ByteConverter.GetBytes("��Ҫ���ܵ�����");



                byte[] dataToEncrypt = ByteConverter.GetBytes("��Ҫ���ܵ�����");

                byte[] encryptedData;

                byte[] decryptedData;



                //����һ�� RSACryptoServiceProvider ��ʵ��������������˽����Կ����

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();



                //ͨ���������ȥ���ܣ�������Կ��Ϣ
                //ʹ��RSACryptoServiceProvider.ExportParameters(false),
                //��һ�� boolean ���������Ƿ��� OAEP ���.

                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);



                MessageBox.Show(Convert.ToBase64String(encryptedData));



                //ͨ���������ȥ����, ˽����Կʹ��
                //RSACryptoServiceProvider.ExportParameters(true),
                //�� һ�� boolean���������Ƿ��� OAEP ���.

                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);



                //��ʾ���ܵ���Ϣ������̨. 

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

                  //���� RSA Key ��Ϣ. ���� public key ��Ϣ.
                  RSA.ImportParameters(RSAKeyInfo);

     
                  //��������ֽ�������Ƿ��� OAEP �����
                  //OAEP ��������Microsoft Windows XP ����
                  //�Ժ�İ汾������

                  return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);

             }
             catch(CryptographicException e)
             {
                  Console.WriteLine(e.Message);
                  return null;
             }

         }

 

          /// <summary>
          /// ���Գƽ����㷨
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

                  //���� RSA Key ��Ϣ. �����Ҫ����
                  // private key ��Ϣ

                  RSA.ImportParameters(RSAKeyInfo);

                  //��������ֽ�������Ƿ��� OAEP ���. 

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Controladores
{
    public class Crypt
    {
        private string _secretKey = string.Empty;
        public Crypt(String _secretKey)
        {
            this._secretKey = _secretKey;
        }

        #region " EncryptString "
        public  string EncryptString(string sInputString)
        {
            return EncryptString(sInputString, _secretKey);
        }

        public  string EncryptString(string sInputString, string sSecretKey)
        {
            try
            {
                TripleDESCryptoServiceProvider tdcEncrypt = new TripleDESCryptoServiceProvider();
                // Fiquem el text a encriptar a una matriu de bytes
                byte[] bInputEncrypt = Encoding.UTF8.GetBytes(sInputString);
                // Creem els objectes d'encriptació amb la clau proporcionada
                MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
                tdcEncrypt.Key = md5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(sSecretKey));
                tdcEncrypt.Mode = CipherMode.ECB;
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, tdcEncrypt.CreateEncryptor(), CryptoStreamMode.Write);
                // Agafem la matriu de bytes i la fiquem dins del fluxe de dades que acabarà
                // a la memòria
                csEncrypt.Write(bInputEncrypt, 0, bInputEncrypt.Length);
                csEncrypt.FlushFinalBlock();
                // Agafem les dades de la memòria i les fiquem dins un text
                StringBuilder sbOutputString = new StringBuilder();
                byte[] bOutputEncrypt = msEncrypt.ToArray();
                msEncrypt.Close();
                for (int iCounter = 0; iCounter < bOutputEncrypt.Length; iCounter++)
                {
                    // Format hexadecimal
                    sbOutputString.AppendFormat("{0:x2}", bOutputEncrypt[iCounter]);
                }
                return sbOutputString.ToString().ToUpper();
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                return "0";
            }
            catch (System.Exception e)
            {
                return "0";
            }

        }
        #endregion

        #region " DecryptString "
        public  string DecryptString(string sInputString)
        {
            return DecryptString(sInputString, _secretKey);
        }

        public  string DecryptString(string sInputString, string sSecretKey)
        {
            try
            {
                TripleDESCryptoServiceProvider tdcDecrypt = new TripleDESCryptoServiceProvider();
                byte[] bInputDecrypt = new byte[Convert.ToInt32((sInputString.Length / 2))];
                MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
                tdcDecrypt.Key = md5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(sSecretKey));
                tdcDecrypt.Mode = CipherMode.ECB;
                int iCounter = 0;
                int iDecrypt = 0;
                ByteConverter bcDecrypt = default(ByteConverter);
                for (iCounter = 0; iCounter < bInputDecrypt.Length; iCounter++)
                {
                    iDecrypt = (Convert.ToInt32(sInputString.Substring(iCounter * 2, 2), 16));
                    bcDecrypt = new ByteConverter();
                    bInputDecrypt[iCounter] = new byte();
                    bInputDecrypt[iCounter] = (byte)bcDecrypt.ConvertTo(iDecrypt, typeof(byte));
                }
                MemoryStream msDecrypt = new MemoryStream();
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, tdcDecrypt.CreateDecryptor(), CryptoStreamMode.Write);
                csDecrypt.Write(bInputDecrypt, 0, bInputDecrypt.Length);
                csDecrypt.FlushFinalBlock();

                StringBuilder sbOutputString = new StringBuilder();
                byte[] bOutputDecrypt = msDecrypt.ToArray();
                msDecrypt.Close();

                char[] characters = Encoding.UTF8.GetChars(bOutputDecrypt);

                for (iCounter = 0; iCounter < characters.Length; iCounter++)
                    sbOutputString.Append(characters[iCounter]);

                return sbOutputString.ToString();
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                return "0";
            }
            catch (System.Exception e)
            {
                return "0";
            }
        }

        #endregion
    }
}
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.IO.Compression;

public class ZipUtil :System.Object 
{
	public static void CopyTo(Stream src, Stream dest) {
		byte[] bytes = new byte[4096];
		
		int cnt;
		
		while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
			dest.Write(bytes, 0, cnt);
		}
	}
	public static byte[] Zip(string str) 
	{
		var bytes = Encoding.UTF8.GetBytes(str);
		return CLZF2.Compress(bytes);	
	}
	
	public static string Unzip(byte[] bytes) {
						
			return Encoding.UTF8.GetString(CLZF2.Decompress(bytes));
	}
}

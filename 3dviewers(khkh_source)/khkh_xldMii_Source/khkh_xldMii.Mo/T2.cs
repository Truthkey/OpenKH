using System;
using System.Collections.Generic;
namespace khkh_xldMii.Mo
{
	public class T2
	{
		public int c00;
		public int c01;
		public int c02;
		public int c03;
		public int c04;
		public List<T9f> al9f = new List<T9f>();
		public T2(int c00, int c01, int c02, int c03, int c04)
		{
			this.c00 = c00;
			this.c01 = c01;
			this.c02 = c02;
			this.c03 = c03;
			this.c04 = c04;
		}
		public override string ToString()
		{
			return string.Format("{0:X2} {1:X2} {2:X2} {3:X2} {4:X4}", new object[]
			{
				this.c00,
				this.c01,
				this.c02,
				this.c03,
				this.c04
			});
		}
	}
}

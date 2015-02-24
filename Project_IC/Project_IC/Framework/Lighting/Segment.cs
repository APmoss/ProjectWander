using System;
using System.Collections.Generic;

namespace Project_IC.Framework.Lighting {
	class Segment {
		#region Fields
		public EndPoint P1 = new EndPoint();
		public EndPoint P2 = new EndPoint();
		#endregion

		public Segment() { }
		public Segment(EndPoint p1, EndPoint p2) {
			this.P1 = p1;
			this.P2 = p2;
		}
	}
}

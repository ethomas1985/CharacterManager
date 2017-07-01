using System;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Model
{
	internal class Die : IDie, IEquatable<IDie>
	{
		public Die(int pFaces)
		{
			Faces = pFaces;
		}

		public int Faces { get; }

		public override string ToString()
		{
			return $"d{Faces}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IDie);
		}

		public bool Equals(IDie pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}
			return Faces == pOther.Faces;
		}

		public override int GetHashCode()
		{
			return Faces;
		}
	}
}

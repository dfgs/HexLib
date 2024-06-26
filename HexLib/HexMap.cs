﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HexLib
{
	public class HexMap
	{

		protected int[,] distances;

		protected HexMap()
		{
			
		}

		
		public static int GetPerimeter(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			if (Radius == 0) return 1;
			return 6 * Radius;
		}
		public static int GetMapSize(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			return Radius * (Radius + 1) / 2 * 6 + 1;
		}

		public int GetDistanceOld(HexCoordinate CoordinateA, HexCoordinate CoordinateB)
		{
			return distances[CoordinateA.Index, CoordinateB.Index];
		}

		public int GetDistance(HexCoordinate CoordinateA, HexCoordinate CoordinateB)
		{
			double angle;

			if (CoordinateA.Radius == 0) return CoordinateB.Radius;
			if (CoordinateB.Radius == 0) return CoordinateA.Radius;

			angle = GetAngle(CoordinateA, CoordinateB);
			if (angle >= 120) return CoordinateA.Radius + CoordinateB.Radius;
			if (angle <= 30) return Math.Abs(CoordinateA.Radius - CoordinateB.Radius);
			return -1;
		}

		public double GetAngle(HexCoordinate Coordinate)
		{
			double result;
			
			if (Coordinate.Radius == 0) return 0;
			result = Coordinate.RingIndex *360 / GetPerimeter(Coordinate.Radius) ;

			return result;
		}
		public double GetAngle(HexCoordinate CoordinateA, HexCoordinate CoordinateB)
		{
			double angleA,angleB;
			double result;


			angleA = GetAngle(CoordinateA);
			angleB=GetAngle(CoordinateB);

			if ((CoordinateA.Radius == 0) || (CoordinateB.Radius == 0)) return 0;

			result = Math.Abs((angleA - angleB)%360);
			if (result > 180) result = 360 - result;

			return result;
			
		}
	}



	public class HexMap<T> : HexMap,IHexMap<T>
	{
		private T[] items;
		public T this[HexCoordinate Coordinate]
		{
			get
			{
				return items[Coordinate.Index];
			}
			set
			{
				items[Coordinate.Index] = value;
			}
		}
		public T this[int Index]
		{
			get
			{
				return items[Index];
			}
			set
			{
				items[Index] = value;
			}
		}

		public int Radius
		{
			get;
			private set;
		}

		public int Count
		{
			get;
			private set;
		}

		public HexMap(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			this.Radius = Radius;
			Count = HexMap.GetMapSize(Radius);
			items = new T[Count];

			distances = new int[Count, Count];
			for(int x=0;x<Count;x++)
			{
				for (int y = 0; y < Count; y++)
				{
					distances[x, y] = int.MaxValue;
				}
			}

			for (int r = 0; r <= Radius; r++)
			{
				for (int i = 0; i < GetPerimeter(r); i++)
				{
					HexCoordinate coordinate = new HexCoordinate(r, i);
					distances[coordinate.Index, coordinate.Index] = 0;
					distances[0, coordinate.Index] = r;
					distances[coordinate.Index,0] = r;
				}
			}

			if (Radius>0) for (int r=0;r<=Radius;r++)
			{
				for (int i = 0; i < GetPerimeter(r); i++)
				{
					FillDistanceToOld(new HexCoordinate(r, i), new HexCoordinate(r, i), 0);
				}
				
			}

			/*string s = "";
			for (int x = 0; x < Count; x++)
			{
				for (int y = 0; y < Count; y++)
				{
					int d = distances[y, x];
					s += d;
					if (d < 10) s +=  "  "; else s += " ";
				}
				s += "\r\n";
			}

			s = "";*/
		}
		
		public IEnumerable<HexCoordinate> GetNeighbours(HexCoordinate Coordinate)
		{
			return Coordinate.GetNeighbours().Where(item => item.Index < Count);
		}
		public IEnumerable<HexCoordinate> DrawLine(HexCoordinate A, HexCoordinate B)
		{
			bool useMaxIndex=false;
			yield return A;
			
			while(!A.Equals(B))
			{
				A = GetNearestCoordinateFromAToBOld(A, B,useMaxIndex);
				if (A.Index > Count) yield break;
				yield return A;
				useMaxIndex = !useMaxIndex;
			}
		}

		private HexCoordinate GetNearestCoordinateFromAToBOld(HexCoordinate A, HexCoordinate B,bool UseMaxIndex)
		{
			HexCoordinate[] neighbors;
			HexCoordinate candidate;
			List<HexCoordinate> candidates;
			

			int distance,candidateDistance;

			neighbors = GetNeighbours(A).ToArray();
			
			if (neighbors.Contains(B)) return B;

			candidates = new List<HexCoordinate>();
			candidate = neighbors[0];
			candidateDistance = GetDistanceOld(candidate, B);
			candidates.Add(candidate);

			for(int t=1;t<neighbors.Length;t++)
			{
				distance = GetDistanceOld(neighbors[t], B);
				if (distance > candidateDistance) continue;
				if (distance < candidateDistance)
				{
					candidates.Clear();
					candidate = neighbors[t];candidateDistance = distance;
					candidates.Add(candidate);
					continue;
				}
				candidates.Add(neighbors[t]);
			}

			if (UseMaxIndex)
			{
				foreach(HexCoordinate coordinate in candidates)
				{
					if (coordinate.RingIndex < candidate.RingIndex) candidate = coordinate;
				}
			}
			else
			{
				foreach (HexCoordinate coordinate in candidates)
				{
					if (coordinate.RingIndex > candidate.RingIndex) candidate = coordinate;
				}

			}

			return candidate;
		}

		private void FillDistanceToOld(HexCoordinate Origin, HexCoordinate Point, int Dist)
		{
			int minDist;

			minDist = GetNeighbours(Point).Min(neighbourCoordinate => distances[Origin.Index, neighbourCoordinate.Index])+1;
			if (minDist < Dist) Dist = minDist;

			distances[Origin.Index, Point.Index] = Dist;
			distances[Point.Index, Origin.Index] = Dist;

			int newDist = Dist + 1;
			foreach (HexCoordinate neighbourCoordinate in GetNeighbours(Point))
			{
				if (distances[Origin.Index, neighbourCoordinate.Index] > newDist)
				{
					FillDistanceToOld(Origin, neighbourCoordinate, newDist);
				}
			}
			
		}
		public IEnumerator<T> GetEnumerator()
		{
			foreach (T item in items) yield return item;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (T item in items) yield return item;
		}

		
	}

}

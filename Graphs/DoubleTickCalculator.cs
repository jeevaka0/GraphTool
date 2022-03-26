using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
	// Ideally these calculations should be done in a base class shared by both X and Y Double axis. However we can't inherit multiple times or specialize Axis class.
	class DoubleTickCalculator
	{
		static List<Decimal> m_MajorTicks = new List<Decimal>() { 1, 2, 5 };
		static Decimal m_MinTicks = 7;

		// We use Decimal to avoid rounding errors. 'tick' returned will be one of the values in list above.
		protected static void getTickSize( double min, double max, out Decimal tick, out Decimal exponent )
		{
			// The goal is to display at least 7 major ticks. Assuming both min and max do not fall on major ticks, those two values will fall outside the 
			// those 7 tick range. The reason to choose 7 is that the range [0-1x10^n] will be split into 1x10^(n-1) ticks rather than 2x10^(n-1) ticks.
			// 7 is the smallest number that achive this goal. We like to have less ticks to reduce clutter.
			double range = ( max - min );

			// Lets first try to express range as a number between in the range [7,70) along with a power of 10.
			// No Log10 

			exponent = Math.Floor( (Decimal)Math.Log10( range ) );									// Rounds down. E.g. -0.9 to -1.
			Decimal modifiedRange = (Decimal) ( range / Math.Pow( 10, (double)exponent ) );			// now modifiedRange >= 1.
			if ( (Decimal)modifiedRange < m_MinTicks )												// move it to [7,70) range.
			{
				modifiedRange *= 10;
				exponent -= 1;
			}

			tick = m_MajorTicks.FindLast( x => modifiedRange / x >= m_MinTicks );
		}

		protected static List<Decimal> getTickValues( double min, double max, out Decimal exponent, out Decimal decimals )
		{
			List<Decimal> tickValues = new List<decimal>();
			Decimal tick;
			Decimal tickExponent;
			
			getTickSize( min, max, out tick, out tickExponent );								// tick does not have any decimal places here.

			Decimal baseMax = (Decimal)( max / Math.Pow( 10, (double)tickExponent ) );			// Bring 'max' to same base.
			Decimal maxTick = tick * Math.Floor( baseMax / tick );							// Max could lie to the right of maxTick. That is ok.

			// Now maxTick is an integer that can be divided by tick.

			// Next we need to decide what is the most suitable exponent.
			decimals = Math.Floor( (Decimal) Math.Log10( (double)Math.Abs( maxTick ) ) );
			maxTick = maxTick / ( (Decimal)Math.Pow( 10, (double)decimals ) );								// Now maxTick is in the range (-10,-1] or [1,10)
			exponent = tickExponent + decimals;

			// We select an exponent that is a multiple of 3 but not 3.
			if ( 5 < exponent )					// When exponent is 6 or more, we use 10^6, 10^9 etc.
			{
				long result;
				long rem = Math.DivRem( (long)exponent, 3, out result );
				exponent = (Decimal) ( result * 3 );
				maxTick *= (Decimal)Math.Pow( 10, rem );
				decimals -= rem;
				
			}
			else if ( exponent < -2 )			// When an exponent is -3 or less, we use 10
			{
				long result;
				long rem = Math.DivRem( (long)exponent - 2, 3, out result );
				exponent = (Decimal) ( result * 3 );
				maxTick *= (Decimal)Math.Pow( 10, rem + 2 );
				decimals -= ( rem + 2 );
			}
			else
			{
				maxTick *= (Decimal)Math.Pow( 10, (double)exponent );				// -2 <= exponent <= 5 range we don't use an exponent.
				decimals -= exponent;
				exponent = 0;
			}

			Decimal modifiedTick = tick * (Decimal)Math.Pow( 10, (double)( tickExponent - exponent ) );
			Decimal minTick = (Decimal)( max / Math.Pow( 10, (double)exponent ) );							// 'exponent' is our standard now.

			do
			{
				tickValues.Add( minTick );
				minTick += modifiedTick;
			} while ( minTick < maxTick );				// We stop after adding the maxTick.

			return tickValues;
		}


		public static List<string> getTicks( double min, double max, List<double> tickValues )
		{
			List<string> labels = new List<string>();

			Decimal exponent;
			Decimal decimals;

			List<Decimal> tickDecimalValues = getTickValues( min, max, out exponent, out decimals );
			tickValues.Clear();

			double multiplier = Math.Pow( 10, (double)exponent );
			string format = "#." + new String( '0', Convert.ToInt32( decimals ) ) + " E" + exponent.ToString();
			foreach ( Decimal d in tickDecimalValues )
			{
				labels.Add( d.ToString( format ) );
				tickValues.Add( (double)d * multiplier );
			}

			return labels;
		}
	}
}

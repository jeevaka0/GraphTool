using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NetPlus
{
	public class Operators<T>
	{
		// Delegate types.

		// Readonly static delegates.
		protected readonly static DelegateTypes.BinaryFunction<T> m_Add;
		protected readonly static DelegateTypes.BinaryFunction<T> m_Subtract;
		protected readonly static DelegateTypes.BinaryFunction<T> m_Min;
		protected readonly static DelegateTypes.BinaryFunction<T> m_Max;

		protected readonly static DelegateTypes.Comparison<T> m_Equal;
		protected readonly static DelegateTypes.Comparison<T> m_Less;
		protected readonly static DelegateTypes.Comparison<T> m_Greater;


		// Operators exposed as properties.
		public static DelegateTypes.BinaryFunction<T> Add
		{
			get { return m_Add; }
		}

		public static DelegateTypes.BinaryFunction<T> Subtract
		{
			get { return m_Subtract; }
		}

		public static DelegateTypes.Comparison<T> Less
		{
			get { return m_Less; }
		}

		public static DelegateTypes.Comparison<T> Equal
		{
			get { return m_Equal; }
		}

		public static DelegateTypes.Comparison<T> Greater
		{
			get { return m_Greater; }
		}

		public static DelegateTypes.BinaryFunction<T> Min
		{
			get { return m_Min; }
		}

		public static DelegateTypes.BinaryFunction<T> Max
		{
			get { return m_Max; }
		}

		// Initialization of static delegates.
		static Operators()
		{
			ParameterExpression paramA = Expression.Parameter( typeof(T), "a" );
			ParameterExpression paramB = Expression.Parameter( typeof(T), "b" );
			ParameterExpression[] parameters = { paramA, paramB };

			m_Add = BuildBinaryFunction( BinaryExpression.Add( paramA, paramB ), parameters );
			m_Subtract = BuildBinaryFunction( BinaryExpression.Subtract( paramA, paramB ), parameters );

			m_Equal = BuildComparison( BinaryExpression.Equal( paramA, paramB ), parameters );
			m_Less = BuildComparison( BinaryExpression.LessThan( paramA, paramB ), parameters );
			m_Greater = BuildComparison( BinaryExpression.GreaterThan( paramA, paramB ), parameters );

			m_Min = BuildConditionalFunction( Expression.Condition( BinaryExpression.LessThanOrEqual( paramA, paramB ), paramA, paramB ), parameters );
			m_Max = BuildConditionalFunction( Expression.Condition( BinaryExpression.GreaterThanOrEqual( paramA, paramB ), paramA, paramB ), parameters );
		}

		protected static DelegateTypes.BinaryFunction<T> BuildBinaryFunction( BinaryExpression expression, ParameterExpression [] parameters )
		{
			Expression<DelegateTypes.BinaryFunction<T>> lambda = Expression.Lambda<DelegateTypes.BinaryFunction<T>>( expression, parameters );
			return lambda.Compile();
		}

		protected static DelegateTypes.Comparison<T> BuildComparison( BinaryExpression expression, ParameterExpression[] parameters )
		{
			Expression<DelegateTypes.Comparison<T>> lambda = Expression.Lambda<DelegateTypes.Comparison<T>>( expression, parameters );
			return lambda.Compile();
		}

		protected static DelegateTypes.BinaryFunction<T> BuildConditionalFunction( ConditionalExpression expression, ParameterExpression[] parameters )
		{
			Expression<DelegateTypes.BinaryFunction<T>> lambda = Expression.Lambda<DelegateTypes.BinaryFunction<T>>( expression, parameters );
			return lambda.Compile();
		}

	}


	public class Operators<R, T>
	{
		// Readonly static delegates.
		protected readonly static DelegateTypes.Convert<R, T> m_Convert;				// R - source type, T - target type.

		public static DelegateTypes.Convert<R, T> Cast
		{
			get { return m_Convert; }
		}

		static Operators()
		{
			ParameterExpression param = Expression.Parameter( typeof( R ) );

			UnaryExpression convert = UnaryExpression.Convert( param, typeof( T ) );
			Expression<DelegateTypes.Convert<R, T>> lambda = Expression.Lambda<DelegateTypes.Convert<R, T>>( convert, param );
			m_Convert = lambda.Compile();
		}
	}
}

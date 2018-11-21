namespace WebCasino.Service.Utility.Validator
{
	public static class ServiceValidator
	{
		public static bool ObjectIsNotEqualNull(object entity)
		{
			if (entity != null)
			{
				return true;
			}

			return false;
		}

		public static bool ValueNotEqualZero(int value)
		{
			if (value != 0)
			{
				return true;
			}

			return false;
		}

		public static bool IsInputStringEmptyOrNull(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return true;
			}

			return false;
		}

		public static bool ValueIsBetween(int value, int min, int max)
		{
			if (value < min || value > max)
			{
				return false;
			}
			return true;
		}

		public static bool ValueIsBetween(double value, double min, double max)
		{
			if (value < min || value > max)
			{
				return false;
			}
			return true;
		}

		public static bool CheckStringLength(int length, int expected)
		{
			if (length > expected)
			{
				return false;
			}
			return true;
		}
	}
}

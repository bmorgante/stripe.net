﻿using System.Collections.Generic;

namespace Stripe
{
	public class StripeApplicationFeeService
	{
		private string ApiKey { get; set; }

		public StripeApplicationFeeService(string apiKey = null)
		{
			ApiKey = apiKey;
		}

		public virtual StripeApplicationFee Get(string applicationFeeId)
		{
			var url = string.Format("{0}/{1}", Urls.ApplicationFees, applicationFeeId);

			var response = Requestor.GetString(url, ApiKey);

			return Mapper<StripeApplicationFee>.MapFromJson(response);
		}

		public virtual StripeApplicationFee Refund(string applicationFeeId, int? refundAmountInCents = null)
		{
			var url = string.Format("{0}/{1}/refund", Urls.ApplicationFees, applicationFeeId);

			if (refundAmountInCents.HasValue)
				url = ParameterBuilder.ApplyParameterToUrl(url, "amount", refundAmountInCents.Value.ToString());

			var response = Requestor.PostString(url, ApiKey);

			return Mapper<StripeApplicationFee>.MapFromJson(response);
		}

		public virtual IEnumerable<StripeApplicationFee> List(int count = 10, int offset = 0, string chargeId = null)
		{
			var url = Urls.ApplicationFees;
			url = ParameterBuilder.ApplyParameterToUrl(url, "count", count.ToString());
			url = ParameterBuilder.ApplyParameterToUrl(url, "offset", offset.ToString());

			if (!string.IsNullOrEmpty(chargeId))
				url = ParameterBuilder.ApplyParameterToUrl(url, "charge", chargeId);

			var response = Requestor.GetString(url, ApiKey);

			return Mapper<StripeApplicationFee>.MapCollectionFromJson(response);
		}
	}
}
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen.AuthReq
{
	public class CustomRequireClaims : IAuthorizationRequirement
	{
		public string claimType { get; }

		public CustomRequireClaims(string claimType)
		{
			this.claimType = claimType;
		}
	}

	public class CustumRequireClaimHandler : AuthorizationHandler<CustomRequireClaims>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaims requirement)
		{
			var hasClaim = context.User.Claims.Any(u => u.Type == requirement.claimType);
			if(hasClaim)
				context.Succeed(requirement);

			return Task.CompletedTask;

		}
	}
}

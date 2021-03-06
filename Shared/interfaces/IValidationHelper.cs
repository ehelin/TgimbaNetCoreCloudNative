﻿using Shared.dto.api;

namespace Shared.interfaces
{
    public interface IValidationHelper
    {
        //TODO - add test
        void IsValidRequest(string userName, string EncodedJwtPrivateKey);
        void IsValidRequest(UpsertBucketListItemRequest request);
        void IsValidRequest(string EncodedUserName, string EncodedToken, int BucketListItemId);
        void IsValidRequest(GetBucketListItemRequest request);
        void IsValidRequest(TokenRequest request);
        void IsValidRequest(LoginRequest request);
        void IsValidRequest(RegistrationRequest request);
        void IsValidRequest(LogMessageRequest request);
    }
}

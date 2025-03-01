﻿using System.Net;
using System.Threading.Tasks;
using Vonage.Redaction;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class RedactTests : TestBase
    {
        [Theory]
        [InlineData(true, RedactionProduct.Sms, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Sms, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Sms, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.Messages, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Messages, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.NumberInsight, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.NumberInsight, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.Verify, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Verify, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.VerifySdk, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.VerifySdk, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.Voice, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Voice, RedactionType.Outbound)]
        public void Redact(bool passCredentials, RedactionProduct product, RedactionType type)
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "test",
                Product = product,
                Type = type
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            bool response = client.RedactClient.Redact(request, passCredentials ? creds : null);

            //ASSERT
            Assert.True(response);
        }

        [Theory]
        [InlineData(true, RedactionProduct.Sms, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Sms, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Sms, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.Messages, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Messages, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.NumberInsight, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.NumberInsight, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.Verify, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Verify, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.VerifySdk, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.VerifySdk, RedactionType.Outbound)]
        [InlineData(false, RedactionProduct.Voice, RedactionType.Inbound)]
        [InlineData(false, RedactionProduct.Voice, RedactionType.Outbound)]
        public async Task RedactAsync(bool passCredentials, RedactionProduct product, RedactionType type)
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "test",
                Product = product,
                Type = type
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            bool response = await client.RedactClient.RedactAsync(request, passCredentials ? creds : null);

            //ASSERT
            Assert.True(response);
        }


        [Fact]
        public void RedactReturns401()
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "209ab3c7536542b91e8b5aef032f6861",
                Product = RedactionProduct.Sms,
                Type = RedactionType.Inbound
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent, expectedCode: HttpStatusCode.Unauthorized);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            var exception = Assert.Throws<VonageHttpRequestException>(() => client.RedactClient.Redact(request));

            //ASSERT
            Assert.NotNull(exception);
            Assert.Equal(expectedResponseContent, exception.Json);
        }

        [Fact]
        public void RedactReturns403()
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "209ab3c7536542b91e8b5aef032f6861",
                Product = RedactionProduct.Sms,
                Type = RedactionType.Inbound
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent, expectedCode: HttpStatusCode.Forbidden);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            var exception = Assert.Throws<VonageHttpRequestException>(() => client.RedactClient.Redact(request));

            //ASSERT
            Assert.NotNull(exception);
            Assert.Equal(expectedResponseContent, exception.Json);
        }

        [Fact]
        public void RedactReturns404()
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "209ab3c7536542b91e8b5aef032f6861",
                Product = RedactionProduct.Sms,
                Type = RedactionType.Inbound
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent, expectedCode: HttpStatusCode.NotFound);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            var exception = Assert.Throws<VonageHttpRequestException>(() => client.RedactClient.Redact(request));

            //ASSERT
            Assert.NotNull(exception);
            Assert.Equal(expectedResponseContent, exception.Json);
        }

#if (NETCOREAPP2_1_OR_GREATER)

        [Fact]
        public void RedactReturns422()
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "209ab3c7536542b91e8b5aef032f6861",
                Product = RedactionProduct.Sms,
                Type = RedactionType.Inbound
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent, expectedCode: HttpStatusCode.UnprocessableEntity);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            var exception = Assert.Throws<VonageHttpRequestException>(() => client.RedactClient.Redact(request));

            //ASSERT
            Assert.NotNull(exception);
            Assert.Equal(expectedResponseContent, exception.Json);
        }

        [Fact]
        public void RedactReturns429()
        {
            //ARRANGE
            RedactRequest request = new RedactRequest
            {
                Id = "209ab3c7536542b91e8b5aef032f6861",
                Product = RedactionProduct.Sms,
                Type = RedactionType.Inbound
            };
            var expectedResponseContent = GetExpectedJson();

            string expectedUri = $"{ApiUrl}/v1/redact/transaction";
            Setup(expectedUri, expectedResponseContent, expectedCode: HttpStatusCode.TooManyRequests);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            var exception = Assert.Throws<VonageHttpRequestException>(() => client.RedactClient.Redact(request));

            //ASSERT
            Assert.NotNull(exception);
            Assert.Equal(expectedResponseContent, exception.Json);
        }
#endif
    }

}

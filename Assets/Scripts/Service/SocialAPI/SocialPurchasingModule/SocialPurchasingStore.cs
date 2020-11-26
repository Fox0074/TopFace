using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine;
using HappyGames.SocialAPI;
using System.Linq;

namespace HappyGames.Purchasing
{
    public class SocialPurchasingStore : AbstractStore
    {
        public const string STORE_NAME = "SocialStore";

        private IStoreCallback _callback;
        //private LocalizationService _localizationService;
        //private Server _server;
        private SocialAPIManager _socialAPIManager;
        private List<ProductDescription> _products;

        public SocialPurchasingStore(SocialAPIManager socialApiManager)
        {
            // _localizationService = localizationService;
            // _server = server;
            _socialAPIManager = socialApiManager;
            _products = new List<ProductDescription>();
        }

        public override void Initialize(IStoreCallback callback)
        {
            _callback = callback;
            _socialAPIManager.ProductPurchased += OnSocialProductPurchased;
            _socialAPIManager.ProductPurchaseFailed += OnSocialProductPurchaseFailed;
        }

        public override async void RetrieveProducts(ReadOnlyCollection<ProductDefinition> definitions)
        {
            //var serverProductsResponse = await _server.LoadProductsAsync(_localizationService.Locale);
            //if (serverProductsResponse?.Status != ServerResponseStatus.Success || serverProductsResponse.Data == null)
            // {
            //     Debug.LogError("Unable to load server products definitions");
            //     _callback.OnSetupFailed(InitializationFailureReason.NoProductsAvailable);
            //     return;
            // }

            // var transactionsResponse = await _server.LoadTransactionsAsync();
            // if (transactionsResponse?.Status != ServerResponseStatus.Success || transactionsResponse.Data == null)
            // {
            //     Debug.LogError("Unable to load server transactions");
            //     _callback.OnSetupFailed(InitializationFailureReason.NoProductsAvailable);
            //     return;
            // }

            // var serverProducts = serverProductsResponse.Data;
            //var transactions = transactionsResponse.Data;

            foreach (var definition in definitions)
            {
                // var serverProduct = serverProducts.FirstOrDefault(product => product.ID.ToString() == definition.storeSpecificId);
                // if (serverProduct == null)
                // {
                //     Debug.LogWarningFormat("Unable to find server definition for product {0}:{1}", definition.id, definition.storeSpecificId);
                //     continue;
                // }

                // var transaction = transactions.FirstOrDefault(trans =>
                //     trans.ProductId.ToString() == definition.storeSpecificId && !trans.Completed);

                // var metadata = new ProductMetadata(
                //     string.Format("{0} {1}", serverProduct.LocalizedPrice, serverProduct.CurrencyCode),
                //     serverProduct.LocalizedTitle,
                //     serverProduct.LocalizedDescription,
                //     serverProduct.CurrencyCode,
                //     serverProduct.LocalizedPrice
                // );

                //ProductDescription productDescription;

                // if (transaction != null)
                // {
                //     productDescription = new ProductDescription(
                //         definition.storeSpecificId,
                //         metadata,
                //         "{}",
                //         transaction.TransactionId
                //     );
                // }
                // else
                // {
                //     productDescription = new ProductDescription(
                //         definition.storeSpecificId,
                //         metadata
                //     );
                // }

                //_products.Add(productDescription);
            }

            _callback.OnProductsRetrieved(_products);
        }

        public override void Purchase(ProductDefinition definition, string developerPayload)
        {
            var product = _products.FirstOrDefault(prod => prod.storeSpecificId == definition.storeSpecificId);
            if (product == null)
            {
                Debug.LogError("Attempting to make a purchase with wrong product ID");
                return;
            }

            _socialAPIManager.BuyProduct(
                product.storeSpecificId,
                new SocialPurchaseData
                {
                    ProductTitle = product.metadata.localizedTitle,
                    ProductDescription = product.metadata.localizedDescription,
                    ProductPrice = product.metadata.localizedPrice
                }
            );
        }

        private void OnSocialProductPurchased(string productId, string transactionId)
        {
            _callback.OnPurchaseSucceeded(productId, "", transactionId);
        }

        private void OnSocialProductPurchaseFailed(string productId, string error)
        {
            _callback.OnPurchaseFailed(new PurchaseFailureDescription(
                productId,
                PurchaseFailureReason.Unknown,
                error
            ));
        }

        public override void FinishTransaction(ProductDefinition product, string transactionId)
        {
            // _server.MarkTransactionUsed(transactionId).ContinueWith(task =>
            // {
            //     if (task.IsFaulted)
            //         Debug.LogErrorFormat("Mark transaction used exception: {0}", task.Exception);
            //     if (task.Result?.Status != ServerResponseStatus.Success)
            //         Debug.LogErrorFormat("Unable to mark transaction used: {0}", transactionId);
            // });
        }
    }
}

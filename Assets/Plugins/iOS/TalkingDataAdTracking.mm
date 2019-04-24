//
//  TalkingDataAdTracking.mm
//  TalkingData
//
//  Created by Li Weiqiang on 19-4-22.
//  Copyright (c) 2019年 TendCloud. All rights reserved.
//

#import "TalkingDataAppCpa.h"

// Converts C style string to NSString
static NSString *tdatCreateNSString(const char *string) {
    if (string)
        return [NSString stringWithUTF8String:string];
    else
        return nil;
}

extern "C" {
#pragma GCC diagnostic ignored "-Wmissing-prototypes"
    
    void tdatSetVerboseLogDisable() {
        [TalkingDataAppCpa setVerboseLogDisabled];
    }
    
    void tdatBackgroundSessionEnabled() {
        [TalkingDataAppCpa backgroundSessionEnabled];
    }
    
    void tdatEnableSFSafariViewControllerTracking() {
        [TalkingDataAppCpa enableSFSafariViewControllerTracking];
    }
    
    void tdatInit(const char *appId, const char *channelId) {
        [TalkingDataAppCpa init:tdatCreateNSString(appId)
                  withChannelId:tdatCreateNSString(channelId)];
    }
    
    void tdatOnReceiveDeepLink(const char *url) {
        [TalkingDataAppCpa onReceiveDeepLink:[NSURL URLWithString:tdatCreateNSString(url)]];
    }
    
    void tdatOnAdSearch(const char *adSearchJson) {
        NSString *adSearchStr = tdatCreateNSString(adSearchJson);
        NSData *adSearchData = [adSearchStr dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *adSearchDic = [NSJSONSerialization JSONObjectWithData:adSearchData options:0 error:nil];
        TDAdSearch *adSearch = [[TDAdSearch alloc] init];
        adSearch.destination = [adSearchDic objectForKey:@"destination"];
        adSearch.origin = [adSearchDic objectForKey:@"origin"];
        adSearch.itemId = [adSearchDic objectForKey:@"itemId"];
        adSearch.itemLocationId = [adSearchDic objectForKey:@"itemLocationId"];
        adSearch.startDate = [adSearchDic objectForKey:@"startDate"];
        adSearch.endDate = [adSearchDic objectForKey:@"endDate"];
        adSearch.searchTerm = [adSearchDic objectForKey:@"searchTerm"];
        adSearch.googleBusinessVertical = [adSearchDic objectForKey:@"googleBusinessVertical"];
        adSearch.custom = [adSearchDic objectForKey:@"custom"];
        [TalkingDataAppCpa onAdSearch:adSearch];
    }
    
    void tdatOnRegister(const char *account) {
        [TalkingDataAppCpa onRegister:tdatCreateNSString(account)];
    }
    
    void tdatOnLogin(const char *account) {
        [TalkingDataAppCpa onLogin:tdatCreateNSString(account)];
    }
    
    void tdatOnCreateRole(const char *name) {
        [TalkingDataAppCpa onCreateRole:tdatCreateNSString(name)];
    }
    
    void tdatOnPay(const char *account, const char *orderId, int amount, const char *currencyType, const char *payType) {
        [TalkingDataAppCpa onPay:tdatCreateNSString(account)
                     withOrderId:tdatCreateNSString(orderId)
                      withAmount:amount
                withCurrencyType:tdatCreateNSString(currencyType)
                     withPayType:tdatCreateNSString(payType)];
    }
    
    void tdatOnPlaceOrder(const char *account, const char *orderJson) {
        NSString *orderStr = tdatCreateNSString(orderJson);
        NSData *orderData = [orderStr dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *orderDic = [NSJSONSerialization JSONObjectWithData:orderData options:0 error:nil];
        TDOrder *order = [TDOrder orderWithOrderId:orderDic[@"orderId"]
                                             total:[orderDic[@"total"] intValue]
                                      currencyType:orderDic[@"currencyType"]];
        NSArray *items = orderDic[@"items"];
        for (NSDictionary *item in items) {
            [order addItemWithCategory:item[@"category"]
                                itemId:item[@"itemId"]
                                  name:item[@"name"]
                             unitPrice:[item[@"unitPrice"] intValue]
                                amount:[item[@"amount"] intValue]];
        }
        [TalkingDataAppCpa onPlaceOrder:tdatCreateNSString(account)
                              withOrder:order];
    }
    
    void tdatOnOrderPaySucc(const char *account, const char *orderId, int amount, const char *currencyType, const char *payType) {
        [TalkingDataAppCpa onOrderPaySucc:tdatCreateNSString(account)
                              withOrderId:tdatCreateNSString(orderId)
                               withAmount:amount
                         withCurrencyType:tdatCreateNSString(currencyType)
                              withPayType:tdatCreateNSString(payType)];
    }
    
    void tdatOnViewItem(const char *itemId, const char *category, const char *name, int unitPrice) {
        [TalkingDataAppCpa onViewItemWithCategory:tdatCreateNSString(category)
                                           itemId:tdatCreateNSString(itemId)
                                             name:tdatCreateNSString(name)
                                        unitPrice:unitPrice];
    }
    
    void tdatOnAddItemToShoppingCart(const char *itemId, const char *category, const char *name, int unitPrice, int amount) {
        [TalkingDataAppCpa onAddItemToShoppingCartWithCategory:tdatCreateNSString(category)
                                                        itemId:tdatCreateNSString(itemId)
                                                          name:tdatCreateNSString(name)
                                                     unitPrice:unitPrice
                                                        amount:amount];
    }
    
    void tdatOnViewShoppingCart(const char *shoppingCartJson) {
        NSString *shoppingCartStr = tdatCreateNSString(shoppingCartJson);
        NSData *shoppingCartData = [shoppingCartStr dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *shoppingCartDic = [NSJSONSerialization JSONObjectWithData:shoppingCartData options:0 error:nil];
        TDShoppingCart *shoppingCart = [TDShoppingCart createShoppingCart];
        NSArray *items = shoppingCartDic[@"items"];
        for (NSDictionary *item in items) {
            [shoppingCart addItemWithCategory:item[@"category"]
                                       itemId:item[@"itemId"]
                                         name:item[@"name"]
                                    unitPrice:[item[@"unitPrice"] intValue]
                                       amount:[item[@"amount"] intValue]];
        }
        [TalkingDataAppCpa onViewShoppingCart:shoppingCart];
    }
    
    void tdatOnCustEvent1() {
        [TalkingDataAppCpa onCustEvent1];
    }
    
    void tdatOnCustEvent2() {
        [TalkingDataAppCpa onCustEvent2];
    }
    
    void tdatOnCustEvent3() {
        [TalkingDataAppCpa onCustEvent3];
    }
    
    void tdatOnCustEvent4() {
        [TalkingDataAppCpa onCustEvent4];
    }
    
    void tdatOnCustEvent5() {
        [TalkingDataAppCpa onCustEvent5];
    }
    
    void tdatOnCustEvent6() {
        [TalkingDataAppCpa onCustEvent6];
    }
    
    void tdatOnCustEvent7() {
        [TalkingDataAppCpa onCustEvent7];
    }
    
    void tdatOnCustEvent8() {
        [TalkingDataAppCpa onCustEvent8];
    }
    
    void tdatOnCustEvent9() {
        [TalkingDataAppCpa onCustEvent9];
    }
    
    void tdatOnCustEvent10() {
        [TalkingDataAppCpa onCustEvent10];
    }
    
#pragma GCC diagnostic warning "-Wmissing-prototypes"
}

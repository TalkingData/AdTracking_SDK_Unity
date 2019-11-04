//
//  TalkingDataAdTracking.mm
//  TalkingData
//
//  Created by Li Weiqiang on 19-4-22.
//  Copyright (c) 2019年 TendCloud. All rights reserved.
//

#import "TalkingDataAppCpa.h"

//#define TDAT_RETAIL     // 电商零售
//#define TDAT_GAME       // 游戏娱乐
//#define TDAT_FINANCE    // 金融借贷
//#define TDAT_TOUR       // 旅游出行
//#define TDAT_ONLINEEDU  // 在线教育
//#define TDAT_READING    // 小说阅读
//#define TDAT_OTHER      // 其他行业
//#define TDAT_CUSTOM     // 自定义事件

// Converts C style string to NSString
static NSString *tdatCreateNSString(const char *string) {
    return string ? [NSString stringWithUTF8String:string] : nil;
}

extern "C" {
#pragma GCC diagnostic ignored "-Wmissing-prototypes"

void tdatSetVerboseLogDisable() {
    [TalkingDataAppCpa setVerboseLogDisable];
}

void tdatBackgroundSessionEnabled() {
    [TalkingDataAppCpa backgroundSessionEnabled];
}

void tdatEnableSFSafariViewControllerTracking() {
    [TalkingDataAppCpa enableSFSafariViewControllerTracking];
}

void tdatInit(const char *appId, const char *channelId, const char *custom) {
    [TalkingDataAppCpa init:tdatCreateNSString(appId)
              withChannelId:tdatCreateNSString(channelId)
                     custom:tdatCreateNSString(custom)];
}

void tdatOnRegister(const char *account, const char *invitationCode) {
    [TalkingDataAppCpa onRegister:tdatCreateNSString(account)
                   invitationCode:tdatCreateNSString(invitationCode)];
}

void tdatOnLogin(const char *account) {
    [TalkingDataAppCpa onLogin:tdatCreateNSString(account)];
}

void tdatOnCreateCard(const char *account, const char *method, const char *content) {
    [TalkingDataAppCpa onCreateCard:tdatCreateNSString(account)
                             method:tdatCreateNSString(method)
                            content:tdatCreateNSString(content)];
}

void tdatOnReceiveDeepLink(const char *url) {
    [TalkingDataAppCpa onReceiveDeepLink:[NSURL URLWithString:tdatCreateNSString(url)]];
}

void tdatOnFavorite(const char *category, const char *content) {
    [TalkingDataAppCpa onFavorite:tdatCreateNSString(category)
                          content:tdatCreateNSString(content)];
}

void tdatOnShare(const char *account, const char *content) {
    [TalkingDataAppCpa onShare:tdatCreateNSString(account)
                       content:tdatCreateNSString(content)];
}

void tdatOnPunch(const char *account, const char *punchId) {
    [TalkingDataAppCpa onPunch:tdatCreateNSString(account)
                       punchId:tdatCreateNSString(punchId)];
}

void tdatOnSearch(const char *searchJson) {
    NSString *searchStr = tdatCreateNSString(searchJson);
    NSData *searchData = [searchStr dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *searchDic = [NSJSONSerialization JSONObjectWithData:searchData options:0 error:nil];
    TDSearch *search = [[TDSearch alloc] init];
    search.category = [searchDic objectForKey:@"category"];
    search.content = [searchDic objectForKey:@"content"];
#ifdef TDAT_RETAIL
    search.itemId = [searchDic objectForKey:@"itemId"];
    search.itemLocationId = [searchDic objectForKey:@"itemLocationId"];
#endif
#ifdef TDAT_TOUR
    search.destination = [searchDic objectForKey:@"destination"];
    search.origin = [searchDic objectForKey:@"origin"];
    search.startDate = [[searchDic objectForKey:@"startDate"] longLongValue];
    search.endDate = [[searchDic objectForKey:@"endDate"] longLongValue];
#endif
    [TalkingDataAppCpa onSearch:search];
}

#if (defined(TDAT_RETAIL) || defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_ONLINEEDU))
void tdatOnContact(const char *account, const char *content) {
    [TalkingDataAppCpa onContact:tdatCreateNSString(account)
                         content:tdatCreateNSString(content)];
}
#endif

#if (defined(TDAT_GAME) || defined(TDAT_TOUR) || defined(TDAT_ONLINEEDU) || defined(TDAT_READING) || defined(TDAT_OTHER))
void tdatOnPay(const char *account, const char *orderId, int amount, const char *currencyType, const char *payType) {
    [TalkingDataAppCpa onPay:tdatCreateNSString(account)
                 withOrderId:tdatCreateNSString(orderId)
                  withAmount:amount
            withCurrencyType:tdatCreateNSString(currencyType)
                 withPayType:tdatCreateNSString(payType)];
}
#endif

#if (defined(TDAT_RETAIL) || defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_ONLINEEDU))
void tdatOnChargeBack(const char *account, const char *orderId, const char *reason, const char *type) {
    [TalkingDataAppCpa onChargeBack:tdatCreateNSString(account)
                            orderId:tdatCreateNSString(orderId)
                             reason:tdatCreateNSString(reason)
                               type:tdatCreateNSString(type)];
}
#endif

#if (defined(TDAT_FINANCE) || defined(TDAT_ONLINEEDU))
void tdatOnReservation(const char *account, const char *reservationId, const char *category, int amount, const char *term) {
    [TalkingDataAppCpa onReservation:tdatCreateNSString(account)
                       reservationId:tdatCreateNSString(reservationId)
                            category:tdatCreateNSString(category)
                              amount:amount
                                term:tdatCreateNSString(term)];
}
#endif

#if (defined(TDAT_RETAIL) || defined(TDAT_TOUR))
void tdatOnBooking(const char *account, const char *bookingId, const char *category, int amount, const char *content) {
    [TalkingDataAppCpa onBooking:tdatCreateNSString(account)
                       bookingId:tdatCreateNSString(bookingId)
                        category:tdatCreateNSString(category)
                          amount:amount
                         content:tdatCreateNSString(content)];
}
#endif

#ifdef TDAT_RETAIL
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
#endif

#ifdef TDAT_FINANCE
void tdatOnCredit(const char *account, int amount, const char *content) {
    [TalkingDataAppCpa onCredit:tdatCreateNSString(account)
                         amount:amount
                        content:tdatCreateNSString(content)];
}

void tdatOnTransaction(const char *account, const char *transactionJson) {
    NSString *transactionStr = tdatCreateNSString(transactionJson);
    NSData *transactionData = [transactionStr dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *transactionDic = [NSJSONSerialization JSONObjectWithData:transactionData options:0 error:nil];
    TDTransaction *transaction = [[TDTransaction alloc] init];
    transaction.transactionId = [transactionDic objectForKey:@"transactionId"];
    transaction.category = [transactionDic objectForKey:@"category"];
    transaction.amount = [[transactionDic objectForKey:@"amount"] intValue];
    transaction.personA = [transactionDic objectForKey:@"personA"];
    transaction.personB = [transactionDic objectForKey:@"personB"];
    transaction.startDate = [[transactionDic objectForKey:@"startDate"] longValue];
    transaction.endDate = [[transactionDic objectForKey:@"endDate"] longValue];
    transaction.currencyType = [transactionDic objectForKey:@"currencyType"];
    transaction.content = [transactionDic objectForKey:@"content"];
    [TalkingDataAppCpa onTransaction:tdatCreateNSString(account)
                         transaction:transaction];
}
#endif

#ifdef TDAT_GAME
void tdatOnCreateRole(const char *name) {
    [TalkingDataAppCpa onCreateRole:tdatCreateNSString(name)];
}

void tdatOnLevelPass(const char *account, const char *levelId) {
    [TalkingDataAppCpa onLevelPass:tdatCreateNSString(account)
                           levelId:tdatCreateNSString(levelId)];
}

void tdatOnGuideFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onGuideFinished:tdatCreateNSString(account)
                               content:tdatCreateNSString(content)];
}
#endif

#ifdef TDAT_ONLINEEDU
void tdatOnLearn(const char *account, const char *course, long long begin, int duration) {
    [TalkingDataAppCpa onLearn:tdatCreateNSString(account)
                        course:tdatCreateNSString(course)
                         begin:begin
                      duration:duration];
}

void tdatOnPreviewFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onPreviewFinished:tdatCreateNSString(account)
                                 content:tdatCreateNSString(content)];
}
#endif

#ifdef TDAT_READING
void tdatOnRead(const char *account, const char *book, long long begin, int duration) {
    [TalkingDataAppCpa onRead:tdatCreateNSString(account)
                         book:tdatCreateNSString(book)
                        begin:begin
                     duration:duration];
}

void tdatOnFreeFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onFreeFinished:tdatCreateNSString(account)
                              content:tdatCreateNSString(content)];
}
#endif

#if (defined(TDAT_GAME) || defined(TDAT_ONLINEEDU))
void tdatOnAchievementUnlock(const char *account, const char *achievementId) {
    [TalkingDataAppCpa onAchievementUnlock:tdatCreateNSString(account)
                             achievementId:tdatCreateNSString(achievementId)];
}
#endif

#if (defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_OTHER))
void tdatOnBrowse(const char *account, const char *content, long long begin, int duration) {
    [TalkingDataAppCpa onBrowse:tdatCreateNSString(account)
                        content:tdatCreateNSString(content)
                          begin:begin
                       duration:duration];
}
#endif

#if (defined(TDAT_RETAIL) || defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_OTHER))
void tdatOnTrialFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onTrialFinished:tdatCreateNSString(account)
                               content:tdatCreateNSString(content)];
}
#endif

#ifdef TDAT_CUSTOM
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
#endif

#pragma GCC diagnostic warning "-Wmissing-prototypes"
}

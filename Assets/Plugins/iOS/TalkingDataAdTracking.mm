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
static NSString *TDATCreateNSString(const char *string) {
    return string ? [NSString stringWithUTF8String:string] : nil;
}

static char *tdatDeviceId = NULL;

extern "C" {
#pragma GCC diagnostic ignored "-Wmissing-prototypes"

const char *TDATGetDeviceId() {
    if (!tdatDeviceId) {
        NSString *deviceId = [TalkingDataAppCpa getDeviceId];
        tdatDeviceId = (char *)calloc(deviceId.length + 1, sizeof(char));
        strcpy(tdatDeviceId, deviceId.UTF8String);
    }
    return tdatDeviceId;
}

void TDATSetVerboseLogDisable() {
    [TalkingDataAppCpa setVerboseLogDisable];
}

void TDATBackgroundSessionEnabled() {
    [TalkingDataAppCpa backgroundSessionEnabled];
}

void TDATEnableSFSafariViewControllerTracking() {
    [TalkingDataAppCpa enableSFSafariViewControllerTracking];
}

void TDATInit(const char *appId, const char *channelId, const char *custom) {
    if ([TalkingDataAppCpa respondsToSelector:@selector(setFrameworkTag:)]) {
        [TalkingDataAppCpa performSelector:@selector(setFrameworkTag:) withObject:@2];
    }
    [TalkingDataAppCpa init:TDATCreateNSString(appId)
              withChannelId:TDATCreateNSString(channelId)
                     custom:TDATCreateNSString(custom)];
}

void TDATOnRegister(const char *account, const char *invitationCode) {
    [TalkingDataAppCpa onRegister:TDATCreateNSString(account)
                   invitationCode:TDATCreateNSString(invitationCode)];
}

void TDATOnLogin(const char *account) {
    [TalkingDataAppCpa onLogin:TDATCreateNSString(account)];
}

void TDATOnCreateCard(const char *account, const char *method, const char *content) {
    [TalkingDataAppCpa onCreateCard:TDATCreateNSString(account)
                             method:TDATCreateNSString(method)
                            content:TDATCreateNSString(content)];
}

void TDATOnReceiveDeepLink(const char *url) {
    [TalkingDataAppCpa onReceiveDeepLink:[NSURL URLWithString:TDATCreateNSString(url)]];
}

void TDATOnFavorite(const char *category, const char *content) {
    [TalkingDataAppCpa onFavorite:TDATCreateNSString(category)
                          content:TDATCreateNSString(content)];
}

void TDATOnShare(const char *account, const char *content) {
    [TalkingDataAppCpa onShare:TDATCreateNSString(account)
                       content:TDATCreateNSString(content)];
}

void TDATOnPunch(const char *account, const char *punchId) {
    [TalkingDataAppCpa onPunch:TDATCreateNSString(account)
                       punchId:TDATCreateNSString(punchId)];
}

void TDATOnSearch(const char *searchJson) {
    NSString *searchStr = TDATCreateNSString(searchJson);
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
void TDATOnContact(const char *account, const char *content) {
    [TalkingDataAppCpa onContact:TDATCreateNSString(account)
                         content:TDATCreateNSString(content)];
}
#endif

#if (defined(TDAT_GAME) || defined(TDAT_TOUR) || defined(TDAT_ONLINEEDU) || defined(TDAT_READING) || defined(TDAT_OTHER))
void TDATOnPay(const char *account, const char *orderId, int amount, const char *currencyType, const char *payType) {
    [TalkingDataAppCpa onPay:TDATCreateNSString(account)
                 withOrderId:TDATCreateNSString(orderId)
                  withAmount:amount
            withCurrencyType:TDATCreateNSString(currencyType)
                 withPayType:TDATCreateNSString(payType)];
}
#endif

#if (defined(TDAT_RETAIL) || defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_ONLINEEDU))
void TDATOnChargeBack(const char *account, const char *orderId, const char *reason, const char *type) {
    [TalkingDataAppCpa onChargeBack:TDATCreateNSString(account)
                            orderId:TDATCreateNSString(orderId)
                             reason:TDATCreateNSString(reason)
                               type:TDATCreateNSString(type)];
}
#endif

#if (defined(TDAT_FINANCE) || defined(TDAT_ONLINEEDU))
void TDATOnReservation(const char *account, const char *reservationId, const char *category, int amount, const char *term) {
    [TalkingDataAppCpa onReservation:TDATCreateNSString(account)
                       reservationId:TDATCreateNSString(reservationId)
                            category:TDATCreateNSString(category)
                              amount:amount
                                term:TDATCreateNSString(term)];
}
#endif

#if (defined(TDAT_RETAIL) || defined(TDAT_TOUR))
void TDATOnBooking(const char *account, const char *bookingId, const char *category, int amount, const char *content) {
    [TalkingDataAppCpa onBooking:TDATCreateNSString(account)
                       bookingId:TDATCreateNSString(bookingId)
                        category:TDATCreateNSString(category)
                          amount:amount
                         content:TDATCreateNSString(content)];
}
#endif

#ifdef TDAT_RETAIL
void TDATOnViewItem(const char *itemId, const char *category, const char *name, int unitPrice) {
    [TalkingDataAppCpa onViewItemWithCategory:TDATCreateNSString(category)
                                       itemId:TDATCreateNSString(itemId)
                                         name:TDATCreateNSString(name)
                                    unitPrice:unitPrice];
}

void TDATOnAddItemToShoppingCart(const char *itemId, const char *category, const char *name, int unitPrice, int amount) {
    [TalkingDataAppCpa onAddItemToShoppingCartWithCategory:TDATCreateNSString(category)
                                                    itemId:TDATCreateNSString(itemId)
                                                      name:TDATCreateNSString(name)
                                                 unitPrice:unitPrice
                                                    amount:amount];
}

void TDATOnViewShoppingCart(const char *shoppingCartJson) {
    NSString *shoppingCartStr = TDATCreateNSString(shoppingCartJson);
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

void TDATOnPlaceOrder(const char *account, const char *orderJson) {
    NSString *orderStr = TDATCreateNSString(orderJson);
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
    [TalkingDataAppCpa onPlaceOrder:TDATCreateNSString(account)
                          withOrder:order];
}

void TDATOnOrderPaySucc(const char *account, const char *orderId, int amount, const char *currencyType, const char *payType) {
    [TalkingDataAppCpa onOrderPaySucc:TDATCreateNSString(account)
                          withOrderId:TDATCreateNSString(orderId)
                           withAmount:amount
                     withCurrencyType:TDATCreateNSString(currencyType)
                          withPayType:TDATCreateNSString(payType)];
}
#endif

#ifdef TDAT_FINANCE
void TDATOnCredit(const char *account, int amount, const char *content) {
    [TalkingDataAppCpa onCredit:TDATCreateNSString(account)
                         amount:amount
                        content:TDATCreateNSString(content)];
}

void TDATOnTransaction(const char *account, const char *transactionJson) {
    NSString *transactionStr = TDATCreateNSString(transactionJson);
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
    [TalkingDataAppCpa onTransaction:TDATCreateNSString(account)
                         transaction:transaction];
}
#endif

#ifdef TDAT_GAME
void TDATOnCreateRole(const char *name) {
    [TalkingDataAppCpa onCreateRole:TDATCreateNSString(name)];
}

void TDATOnLevelPass(const char *account, const char *levelId) {
    [TalkingDataAppCpa onLevelPass:TDATCreateNSString(account)
                           levelId:TDATCreateNSString(levelId)];
}

void TDATOnGuideFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onGuideFinished:TDATCreateNSString(account)
                               content:TDATCreateNSString(content)];
}
#endif

#ifdef TDAT_ONLINEEDU
void TDATOnLearn(const char *account, const char *course, long long begin, int duration) {
    [TalkingDataAppCpa onLearn:TDATCreateNSString(account)
                        course:TDATCreateNSString(course)
                         begin:begin
                      duration:duration];
}

void TDATOnPreviewFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onPreviewFinished:TDATCreateNSString(account)
                                 content:TDATCreateNSString(content)];
}
#endif

#ifdef TDAT_READING
void TDATOnRead(const char *account, const char *book, long long begin, int duration) {
    [TalkingDataAppCpa onRead:TDATCreateNSString(account)
                         book:TDATCreateNSString(book)
                        begin:begin
                     duration:duration];
}

void TDATOnFreeFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onFreeFinished:TDATCreateNSString(account)
                              content:TDATCreateNSString(content)];
}
#endif

#if (defined(TDAT_GAME) || defined(TDAT_ONLINEEDU))
void TDATOnAchievementUnlock(const char *account, const char *achievementId) {
    [TalkingDataAppCpa onAchievementUnlock:TDATCreateNSString(account)
                             achievementId:TDATCreateNSString(achievementId)];
}
#endif

#if (defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_OTHER))
void TDATOnBrowse(const char *account, const char *content, long long begin, int duration) {
    [TalkingDataAppCpa onBrowse:TDATCreateNSString(account)
                        content:TDATCreateNSString(content)
                          begin:begin
                       duration:duration];
}
#endif

#if (defined(TDAT_RETAIL) || defined(TDAT_FINANCE) || defined(TDAT_TOUR) || defined(TDAT_OTHER))
void TDATOnTrialFinished(const char *account, const char *content) {
    [TalkingDataAppCpa onTrialFinished:TDATCreateNSString(account)
                               content:TDATCreateNSString(content)];
}
#endif

#ifdef TDAT_CUSTOM
void TDATOnCustEvent1() {
    [TalkingDataAppCpa onCustEvent1];
}

void TDATOnCustEvent2() {
    [TalkingDataAppCpa onCustEvent2];
}

void TDATOnCustEvent3() {
    [TalkingDataAppCpa onCustEvent3];
}

void TDATOnCustEvent4() {
    [TalkingDataAppCpa onCustEvent4];
}

void TDATOnCustEvent5() {
    [TalkingDataAppCpa onCustEvent5];
}

void TDATOnCustEvent6() {
    [TalkingDataAppCpa onCustEvent6];
}

void TDATOnCustEvent7() {
    [TalkingDataAppCpa onCustEvent7];
}

void TDATOnCustEvent8() {
    [TalkingDataAppCpa onCustEvent8];
}

void TDATOnCustEvent9() {
    [TalkingDataAppCpa onCustEvent9];
}

void TDATOnCustEvent10() {
    [TalkingDataAppCpa onCustEvent10];
}
#endif

#pragma GCC diagnostic warning "-Wmissing-prototypes"
}

## TalkingData AdTracking Unity SDK
Ad Tracking Unity 平台 SDK 由`封装层`和 `Native SDK` 两部分构成，目前GitHub上提供了封装层代码，需要从 [TalkingData官网](https://www.talkingdata.com/spa/sdk/#/config) 下载最新版的 Android 和 iOS 平台 Native SDK，组合使用。

### 集成说明
1. 下载本项目（封装层）到本地；  
2. 访问 [TalkingData官网](https://www.talkingdata.com/spa/sdk/#/config) 下载最新版的 Android 和 iOS 平台 App Analytics SDK（ Native SDK）
	- 方法1：选择 Unity 平台进行功能定制；
	- 方法2：分别选择 Android 和 iOS 平台进行功能定制，请确保两个平台功能项一致；  
	![](apply.png)
3. 将下载的最新版 `Native SDK` 复制到`封装层`中，构成完整的 Unity SDK。  
	- Android 平台  
	将最新的 .jar 文件复制到 `Assets/Plugins/Android` 目录下
	- iOS 平台  
	将最新的 .a 文件复制到 `Assets/Plugins/iOS` 目录下
4. 按 `Native SDK` 功能选项对`封装层`代码进行必要的删减，详见“注意事项”第2条；
5. 将 Unity SDK 集成您需要统计的工程中，并按 [集成文档](http://doc.talkingdata.com/posts/287) 进行必要配置和功能调用。

### 注意事项
1. 分别选择 Android 和 iOS 平台进行功能定制时，请确保两个平台功能项一致。
2. 如果申请 Native SDK 时只选择了部分功能，则需要在本项目中删除未选择功能对应的封装层代码。  
	a) 未选择`自定义事件`功能则删除以下3部分  
	删除 `Assets/TalkingDataScripts/TalkingDataAdTracking.cs` 文件中如下代码：
	
	```
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent1();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent2();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent3();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent4();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent5();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent6();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent7();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent8();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent9();
		
		[DllImport ("__Internal")]
		private static extern void tdatOnCustEvent10();
	```
	```
		public static void OnCustEvent1()
		{
			...
		}
		public static void OnCustEvent2()
		{
			...
		}
		public static void OnCustEvent3()
		{
			...
		}
		public static void OnCustEvent4()
		{
			...
		}
		public static void OnCustEvent5()
		{
			...
		}
		public static void OnCustEvent6()
		{
			...
		}
		public static void OnCustEvent7()
		{
			...
		}
		public static void OnCustEvent8()
		{
			...
		}
		public static void OnCustEvent9()
		{
			...
		}
		public static void OnCustEvent10()
		{
			...
		}
	```
	删除 `Assets/Plugins/iOS/TalkingDataAdTracking.mm` 文件中如下代码：
	
	```
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
	```
	删除 `Assets/Plugins/iOS/TalkingDataAppCpa.h` 文件中如下代码：
	
	```
		+ (void)onCustEvent1;
		+ (void)onCustEvent2;
		+ (void)onCustEvent3;
		+ (void)onCustEvent4;
		+ (void)onCustEvent5;
		+ (void)onCustEvent6;
		+ (void)onCustEvent7;
		+ (void)onCustEvent8;
		+ (void)onCustEvent9;
		+ (void)onCustEvent10;
	```

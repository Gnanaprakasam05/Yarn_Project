; ModuleID = 'obj\Release\110\android\marshal_methods.arm64-v8a.ll'
source_filename = "obj\Release\110\android\marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 8
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [108 x i64] [
	i64 120698629574877762, ; 0: Mono.Android => 0x1accec39cafe242 => 3
	i64 181099460066822533, ; 1: Microcharts.Droid.dll => 0x28364ffda4c4985 => 32
	i64 232391251801502327, ; 2: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 43
	i64 263803244706540312, ; 3: Rg.Plugins.Popup.dll => 0x3a937a743542b18 => 38
	i64 321870029177907512, ; 4: XamEffects.Droid => 0x477831610fe6938 => 51
	i64 702024105029695270, ; 5: System.Drawing.Common => 0x9be17343c0e7726 => 28
	i64 720058930071658100, ; 6: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x9fe29c82844de74 => 17
	i64 872800313462103108, ; 7: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 15
	i64 887546508555532406, ; 8: Microcharts.Forms => 0xc5132d8dc173876 => 33
	i64 996343623809489702, ; 9: Xamarin.Forms.Platform => 0xdd3b93f3b63db26 => 48
	i64 1000557547492888992, ; 10: Mono.Security.dll => 0xde2b1c9cba651a0 => 29
	i64 1120440138749646132, ; 11: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 25
	i64 1400031058434376889, ; 12: Plugin.Permissions.dll => 0x136de8d4787ec4b9 => 37
	i64 1416135423712704079, ; 13: Microcharts => 0x13a71faa343e364f => 31
	i64 1425944114962822056, ; 14: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 1
	i64 1624659445732251991, ; 15: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 10
	i64 1731380447121279447, ; 16: Newtonsoft.Json => 0x18071957e9b889d7 => 34
	i64 1795316252682057001, ; 17: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 11
	i64 1836611346387731153, ; 18: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 43
	i64 1981742497975770890, ; 19: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 20
	i64 2017679330518390632, ; 20: XamEffects => 0x1c003ca936325768 => 50
	i64 2133195048986300728, ; 21: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 34
	i64 2262844636196693701, ; 22: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 15
	i64 2329709569556905518, ; 23: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 19
	i64 2470498323731680442, ; 24: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 13
	i64 2547086958574651984, ; 25: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 42
	i64 2592350477072141967, ; 26: System.Xml.dll => 0x23f9e10627330e8f => 8
	i64 2624866290265602282, ; 27: mscorlib.dll => 0x246d65fbde2db8ea => 4
	i64 2708001411492626562, ; 28: XamEffects.Droid.dll => 0x2594c0efa7464082 => 51
	i64 2801558180824670388, ; 29: Plugin.CurrentActivity.dll => 0x26e1225279a4e0b4 => 35
	i64 2960931600190307745, ; 30: Xamarin.Forms.Core => 0x2917579a49927da1 => 45
	i64 3017704767998173186, ; 31: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 25
	i64 3122911337338800527, ; 32: Microcharts.dll => 0x2b56cf50bf1e898f => 31
	i64 3289520064315143713, ; 33: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 18
	i64 3522470458906976663, ; 34: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 23
	i64 3531994851595924923, ; 35: System.Numerics => 0x31042a9aade235bb => 7
	i64 3609787854626478660, ; 36: Plugin.CurrentActivity => 0x32188aeda587da44 => 35
	i64 3727469159507183293, ; 37: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 22
	i64 3753588484325436954, ; 38: yarn_brokerage.dll => 0x34176cd6d136ee1a => 53
	i64 3790475190611819521, ; 39: XamEffects.dll => 0x349a791a62593c01 => 50
	i64 4525561845656915374, ; 40: System.ServiceModel.Internals => 0x3ece06856b710dae => 26
	i64 4794310189461587505, ; 41: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 42
	i64 4795410492532947900, ; 42: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 23
	i64 5142919913060024034, ; 43: Xamarin.Forms.Platform.Android.dll => 0x475f52699e39bee2 => 47
	i64 5203618020066742981, ; 44: Xamarin.Essentials => 0x4836f704f0e652c5 => 44
	i64 5507995362134886206, ; 45: System.Core.dll => 0x4c705499688c873e => 5
	i64 6085203216496545422, ; 46: Xamarin.Forms.Platform.dll => 0x5472fc15a9574e8e => 48
	i64 6086316965293125504, ; 47: FormsViewGroup.dll => 0x5476f10882baef80 => 30
	i64 6401687960814735282, ; 48: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 19
	i64 6659513131007730089, ; 49: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0x5c6b57e8b6c3e1a9 => 17
	i64 6671798237668743565, ; 50: SkiaSharp => 0x5c96fd260152998d => 39
	i64 6876862101832370452, ; 51: System.Xml.Linq => 0x5f6f85a57d108914 => 9
	i64 6958011980542809616, ; 52: yarn_brokerage.Android => 0x608fd307fb2a9610 => 52
	i64 7488575175965059935, ; 53: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 9
	i64 7635363394907363464, ; 54: Xamarin.Forms.Core.dll => 0x69f6428dc4795888 => 45
	i64 7637365915383206639, ; 55: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 44
	i64 7654504624184590948, ; 56: System.Net.Http => 0x6a3a4366801b8264 => 0
	i64 7820441508502274321, ; 57: System.Data => 0x6c87ca1e14ff8111 => 27
	i64 7836164640616011524, ; 58: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 10
	i64 7927939710195668715, ; 59: SkiaSharp.Views.Android.dll => 0x6e05b32992ed16eb => 40
	i64 8083354569033831015, ; 60: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 18
	i64 8132393369586336849, ; 61: Plugin.InputKit => 0x70dc10aeafef8451 => 36
	i64 8167236081217502503, ; 62: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 2
	i64 8187102936927221770, ; 63: SkiaSharp.Views.Forms => 0x719e6ebe771ab80a => 41
	i64 8626175481042262068, ; 64: Java.Interop => 0x77b654e585b55834 => 2
	i64 9324707631942237306, ; 65: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 11
	i64 9662334977499516867, ; 66: System.Numerics.dll => 0x8617827802b0cfc3 => 7
	i64 9678050649315576968, ; 67: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 13
	i64 9808709177481450983, ; 68: Mono.Android.dll => 0x881f890734e555e7 => 3
	i64 9998632235833408227, ; 69: Mono.Security => 0x8ac2470b209ebae3 => 29
	i64 10038780035334861115, ; 70: System.Net.Http.dll => 0x8b50e941206af13b => 0
	i64 10134541937470629039, ; 71: yarn_brokerage.Android.dll => 0x8ca52032703c64af => 52
	i64 10325666697786115060, ; 72: Xamarin.Forms.Extended.InfiniteScrolling.dll => 0x8f4c2327669f43f4 => 46
	i64 10430153318873392755, ; 73: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 14
	i64 10984620054693331049, ; 74: Plugin.InputKit.dll => 0x987135bda0a0c069 => 36
	i64 11023048688141570732, ; 75: System.Core => 0x98f9bc61168392ac => 5
	i64 11037814507248023548, ; 76: System.Xml => 0x992e31d0412bf7fc => 8
	i64 11162124722117608902, ; 77: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 24
	i64 11529969570048099689, ; 78: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 24
	i64 12451044538927396471, ; 79: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 16
	i64 12466513435562512481, ; 80: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 21
	i64 12538491095302438457, ; 81: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 12
	i64 12963446364377008305, ; 82: System.Drawing.Common.dll => 0xb3e769c8fd8548b1 => 28
	i64 13370592475155966277, ; 83: System.Runtime.Serialization => 0xb98de304062ea945 => 1
	i64 13403416310143541304, ; 84: Microcharts.Droid => 0xba02801ea6c86038 => 32
	i64 13492263892638604996, ; 85: SkiaSharp.Views.Forms.dll => 0xbb3e2686788d9ec4 => 41
	i64 13572454107664307259, ; 86: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 22
	i64 13647894001087880694, ; 87: System.Data.dll => 0xbd670f48cb071df6 => 27
	i64 13959074834287824816, ; 88: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 16
	i64 13967638549803255703, ; 89: Xamarin.Forms.Platform.Android => 0xc1d70541e0134797 => 47
	i64 14020090542213545754, ; 90: Xamarin.Forms.Extended.InfiniteScrolling => 0xc2915e110773bf1a => 46
	i64 14124974489674258913, ; 91: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 12
	i64 14276485911877471601, ; 92: yarn_brokerage => 0xc620444bfa534d71 => 53
	i64 15370334346939861994, ; 93: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 14
	i64 15609085926864131306, ; 94: System.dll => 0xd89e9cf3334914ea => 6
	i64 15810740023422282496, ; 95: Xamarin.Forms.Xaml => 0xdb6b08484c22eb00 => 49
	i64 16154507427712707110, ; 96: System => 0xe03056ea4e39aa26 => 6
	i64 16324796876805858114, ; 97: SkiaSharp.dll => 0xe28d5444586b6342 => 39
	i64 16833383113903931215, ; 98: mscorlib => 0xe99c30c1484d7f4f => 4
	i64 16895806301542741427, ; 99: Plugin.Permissions => 0xea79f6503d42f5b3 => 37
	i64 17001062948826229159, ; 100: Microcharts.Forms.dll => 0xebefe8ad2cd7a9a7 => 33
	i64 17285063141349522879, ; 101: Rg.Plugins.Popup => 0xefe0e158cc55fdbf => 38
	i64 17671790519499593115, ; 102: SkiaSharp.Views.Android => 0xf53ecfd92be3959b => 40
	i64 17704177640604968747, ; 103: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 21
	i64 17710060891934109755, ; 104: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 20
	i64 17882897186074144999, ; 105: FormsViewGroup => 0xf82cd03e3ac830e7 => 30
	i64 17892495832318972303, ; 106: Xamarin.Forms.Xaml.dll => 0xf84eea293687918f => 49
	i64 18129453464017766560 ; 107: System.ServiceModel.Internals.dll => 0xfb98c1df1ec108a0 => 26
], align 8
@assembly_image_cache_indices = local_unnamed_addr constant [108 x i32] [
	i32 3, i32 32, i32 43, i32 38, i32 51, i32 28, i32 17, i32 15, ; 0..7
	i32 33, i32 48, i32 29, i32 25, i32 37, i32 31, i32 1, i32 10, ; 8..15
	i32 34, i32 11, i32 43, i32 20, i32 50, i32 34, i32 15, i32 19, ; 16..23
	i32 13, i32 42, i32 8, i32 4, i32 51, i32 35, i32 45, i32 25, ; 24..31
	i32 31, i32 18, i32 23, i32 7, i32 35, i32 22, i32 53, i32 50, ; 32..39
	i32 26, i32 42, i32 23, i32 47, i32 44, i32 5, i32 48, i32 30, ; 40..47
	i32 19, i32 17, i32 39, i32 9, i32 52, i32 9, i32 45, i32 44, ; 48..55
	i32 0, i32 27, i32 10, i32 40, i32 18, i32 36, i32 2, i32 41, ; 56..63
	i32 2, i32 11, i32 7, i32 13, i32 3, i32 29, i32 0, i32 52, ; 64..71
	i32 46, i32 14, i32 36, i32 5, i32 8, i32 24, i32 24, i32 16, ; 72..79
	i32 21, i32 12, i32 28, i32 1, i32 32, i32 41, i32 22, i32 27, ; 80..87
	i32 16, i32 47, i32 46, i32 12, i32 53, i32 14, i32 6, i32 49, ; 88..95
	i32 6, i32 39, i32 4, i32 37, i32 33, i32 38, i32 40, i32 21, ; 96..103
	i32 20, i32 30, i32 49, i32 26 ; 104..107
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 8; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 8

; Function attributes: "frame-pointer"="non-leaf" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 8
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 8
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-5 @ a8a26c7b003e2524cc98acb2c2ffc2ddea0f6692"}
!llvm.linker.options = !{}

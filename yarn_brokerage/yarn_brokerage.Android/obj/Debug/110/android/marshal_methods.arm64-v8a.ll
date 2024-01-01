; ModuleID = 'obj\Debug\110\android\marshal_methods.arm64-v8a.ll'
source_filename = "obj\Debug\110\android\marshal_methods.arm64-v8a.ll"
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
@assembly_image_cache_hashes = local_unnamed_addr constant [256 x i64] [
	i64 120698629574877762, ; 0: Mono.Android => 0x1accec39cafe242 => 8
	i64 181099460066822533, ; 1: Microcharts.Droid.dll => 0x28364ffda4c4985 => 6
	i64 210515253464952879, ; 2: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 70
	i64 232391251801502327, ; 3: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 93
	i64 263803244706540312, ; 4: Rg.Plugins.Popup.dll => 0x3a937a743542b18 => 14
	i64 295915112840604065, ; 5: Xamarin.AndroidX.SlidingPaneLayout => 0x41b4d3a3088a9a1 => 94
	i64 321870029177907512, ; 6: XamEffects.Droid => 0x477831610fe6938 => 109
	i64 590536689428908136, ; 7: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x83201fd803ec868 => 31
	i64 634308326490598313, ; 8: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 85
	i64 702024105029695270, ; 9: System.Drawing.Common => 0x9be17343c0e7726 => 116
	i64 720058930071658100, ; 10: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x9fe29c82844de74 => 79
	i64 816102801403336439, ; 11: Xamarin.Android.Support.Collections => 0xb53612c89d8d6f7 => 35
	i64 846634227898301275, ; 12: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0xbbfd9583890bb5b => 28
	i64 872800313462103108, ; 13: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 76
	i64 887546508555532406, ; 14: Microcharts.Forms => 0xc5132d8dc173876 => 7
	i64 940822596282819491, ; 15: System.Transactions => 0xd0e792aa81923a3 => 114
	i64 996343623809489702, ; 16: Xamarin.Forms.Platform => 0xdd3b93f3b63db26 => 105
	i64 1000557547492888992, ; 17: Mono.Security.dll => 0xde2b1c9cba651a0 => 121
	i64 1120440138749646132, ; 18: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 107
	i64 1315114680217950157, ; 19: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 65
	i64 1342439039765371018, ; 20: Xamarin.Android.Support.Fragment => 0x12a14d31b1d4d88a => 45
	i64 1400031058434376889, ; 21: Plugin.Permissions.dll => 0x136de8d4787ec4b9 => 13
	i64 1416135423712704079, ; 22: Microcharts => 0x13a71faa343e364f => 5
	i64 1425944114962822056, ; 23: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 2
	i64 1490981186906623721, ; 24: Xamarin.Android.Support.VersionedParcelable => 0x14b1077d6c5c0ee9 => 59
	i64 1624659445732251991, ; 25: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 63
	i64 1636321030536304333, ; 26: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0x16b5614ec39e16cd => 80
	i64 1731380447121279447, ; 27: Newtonsoft.Json => 0x18071957e9b889d7 => 10
	i64 1744702963312407042, ; 28: Xamarin.Android.Support.v7.AppCompat => 0x18366e19eeceb202 => 55
	i64 1795316252682057001, ; 29: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 64
	i64 1836611346387731153, ; 30: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 93
	i64 1860886102525309849, ; 31: Xamarin.Android.Support.v7.RecyclerView.dll => 0x19d3320d047b7399 => 57
	i64 1875917498431009007, ; 32: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 62
	i64 1938067011858688285, ; 33: Xamarin.Android.Support.v4.dll => 0x1ae565add0bd691d => 54
	i64 1981742497975770890, ; 34: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 86
	i64 2017679330518390632, ; 35: XamEffects => 0x1c003ca936325768 => 108
	i64 2133195048986300728, ; 36: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 10
	i64 2136356949452311481, ; 37: Xamarin.AndroidX.MultiDex.dll => 0x1da5dd539d8acbb9 => 90
	i64 2165725771938924357, ; 38: Xamarin.AndroidX.Browser => 0x1e0e341d75540745 => 68
	i64 2262844636196693701, ; 39: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 76
	i64 2284400282711631002, ; 40: System.Web.Services => 0x1fb3d1f42fd4249a => 120
	i64 2329709569556905518, ; 41: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 83
	i64 2470498323731680442, ; 42: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 71
	i64 2479423007379663237, ; 43: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 97
	i64 2497223385847772520, ; 44: System.Runtime => 0x22a7eb7046413568 => 22
	i64 2547086958574651984, ; 45: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 61
	i64 2592350477072141967, ; 46: System.Xml.dll => 0x23f9e10627330e8f => 23
	i64 2624866290265602282, ; 47: mscorlib.dll => 0x246d65fbde2db8ea => 9
	i64 2708001411492626562, ; 48: XamEffects.Droid.dll => 0x2594c0efa7464082 => 109
	i64 2801558180824670388, ; 49: Plugin.CurrentActivity.dll => 0x26e1225279a4e0b4 => 11
	i64 2949706848458024531, ; 50: Xamarin.Android.Support.SlidingPaneLayout => 0x28ef76c01de0a653 => 51
	i64 2960931600190307745, ; 51: Xamarin.Forms.Core => 0x2917579a49927da1 => 102
	i64 2977248461349026546, ; 52: Xamarin.Android.Support.DrawerLayout => 0x29514fb392c97af2 => 44
	i64 3017704767998173186, ; 53: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 107
	i64 3022227708164871115, ; 54: Xamarin.Android.Support.Media.Compat.dll => 0x29f11c168f8293cb => 49
	i64 3122911337338800527, ; 55: Microcharts.dll => 0x2b56cf50bf1e898f => 5
	i64 3289520064315143713, ; 56: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 82
	i64 3311221304742556517, ; 57: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 21
	i64 3522470458906976663, ; 58: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 95
	i64 3531994851595924923, ; 59: System.Numerics => 0x31042a9aade235bb => 20
	i64 3571415421602489686, ; 60: System.Runtime.dll => 0x319037675df7e556 => 22
	i64 3609787854626478660, ; 61: Plugin.CurrentActivity => 0x32188aeda587da44 => 11
	i64 3716579019761409177, ; 62: netstandard.dll => 0x3393f0ed5c8c5c99 => 112
	i64 3727469159507183293, ; 63: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 92
	i64 3753588484325436954, ; 64: yarn_brokerage.dll => 0x34176cd6d136ee1a => 110
	i64 3790475190611819521, ; 65: XamEffects.dll => 0x349a791a62593c01 => 108
	i64 4252163538099460320, ; 66: Xamarin.Android.Support.ViewPager.dll => 0x3b02b8357f4958e0 => 60
	i64 4264996749430196783, ; 67: Xamarin.Android.Support.Transition.dll => 0x3b304ff259fb8a2f => 53
	i64 4349341163892612332, ; 68: Xamarin.Android.Support.DocumentFile => 0x3c5bf6bea8cd9cec => 43
	i64 4416987920449902723, ; 69: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0x3d4c4b1c879b9883 => 34
	i64 4525561845656915374, ; 70: System.ServiceModel.Internals => 0x3ece06856b710dae => 111
	i64 4636684751163556186, ; 71: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 99
	i64 4782108999019072045, ; 72: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0x425d76cc43bb0a2d => 67
	i64 4794310189461587505, ; 73: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 61
	i64 4795410492532947900, ; 74: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 95
	i64 4841234827713643511, ; 75: Xamarin.Android.Support.CoordinaterLayout => 0x432f856d041f03f7 => 37
	i64 4963205065368577818, ; 76: Xamarin.Android.Support.LocalBroadcastManager.dll => 0x44e0d8b5f4b6a71a => 48
	i64 5142919913060024034, ; 77: Xamarin.Forms.Platform.Android.dll => 0x475f52699e39bee2 => 104
	i64 5178572682164047940, ; 78: Xamarin.Android.Support.Print.dll => 0x47ddfc6acbee1044 => 50
	i64 5203618020066742981, ; 79: Xamarin.Essentials => 0x4836f704f0e652c5 => 101
	i64 5205316157927637098, ; 80: Xamarin.AndroidX.LocalBroadcastManager => 0x483cff7778e0c06a => 88
	i64 5288341611614403055, ; 81: Xamarin.Android.Support.Interpolator.dll => 0x4963f6ad4b3179ef => 46
	i64 5348796042099802469, ; 82: Xamarin.AndroidX.Media => 0x4a3abda9415fc165 => 89
	i64 5376510917114486089, ; 83: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 97
	i64 5408338804355907810, ; 84: Xamarin.AndroidX.Transition => 0x4b0e477cea9840e2 => 96
	i64 5439315836349573567, ; 85: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0x4b7c54ef36c5e9bf => 32
	i64 5446034149219586269, ; 86: System.Diagnostics.Debug => 0x4b94333452e150dd => 123
	i64 5507995362134886206, ; 87: System.Core.dll => 0x4c705499688c873e => 18
	i64 5767696078500135884, ; 88: Xamarin.Android.Support.Annotations.dll => 0x500af9065b6a03cc => 33
	i64 5896680224035167651, ; 89: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x51d5376bfbafdda3 => 84
	i64 6044705416426755068, ; 90: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0x53e31b8ccdff13fc => 52
	i64 6085203216496545422, ; 91: Xamarin.Forms.Platform.dll => 0x5472fc15a9574e8e => 105
	i64 6086316965293125504, ; 92: FormsViewGroup.dll => 0x5476f10882baef80 => 3
	i64 6311200438583329442, ; 93: Xamarin.Android.Support.LocalBroadcastManager => 0x5795e35c580c82a2 => 48
	i64 6319713645133255417, ; 94: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 85
	i64 6401687960814735282, ; 95: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 83
	i64 6405879832841858445, ; 96: Xamarin.Android.Support.Vector.Drawable.dll => 0x58e641c4a660ad8d => 58
	i64 6504860066809920875, ; 97: Xamarin.AndroidX.Browser.dll => 0x5a45e7c43bd43d6b => 68
	i64 6548213210057960872, ; 98: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 74
	i64 6588599331800941662, ; 99: Xamarin.Android.Support.v4 => 0x5b6f682f335f045e => 54
	i64 6591024623626361694, ; 100: System.Web.Services.dll => 0x5b7805f9751a1b5e => 120
	i64 6659513131007730089, ; 101: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0x5c6b57e8b6c3e1a9 => 79
	i64 6671798237668743565, ; 102: SkiaSharp => 0x5c96fd260152998d => 15
	i64 6876862101832370452, ; 103: System.Xml.Linq => 0x5f6f85a57d108914 => 24
	i64 6894844156784520562, ; 104: System.Numerics.Vectors => 0x5faf683aead1ad72 => 21
	i64 6958011980542809616, ; 105: yarn_brokerage.Android => 0x608fd307fb2a9610 => 0
	i64 7036436454368433159, ; 106: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x61a671acb33d5407 => 81
	i64 7103753931438454322, ; 107: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 78
	i64 7194160955514091247, ; 108: Xamarin.Android.Support.CursorAdapter.dll => 0x63d6cb45d266f6ef => 40
	i64 7270811800166795866, ; 109: System.Linq => 0x64e71ccf51a90a5a => 127
	i64 7488575175965059935, ; 110: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 24
	i64 7489048572193775167, ; 111: System.ObjectModel => 0x67ee71ff6b419e3f => 124
	i64 7635363394907363464, ; 112: Xamarin.Forms.Core.dll => 0x69f6428dc4795888 => 102
	i64 7637365915383206639, ; 113: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 101
	i64 7654504624184590948, ; 114: System.Net.Http => 0x6a3a4366801b8264 => 1
	i64 7820441508502274321, ; 115: System.Data => 0x6c87ca1e14ff8111 => 113
	i64 7821246742157274664, ; 116: Xamarin.Android.Support.AsyncLayoutInflater => 0x6c8aa67926f72e28 => 34
	i64 7836164640616011524, ; 117: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 63
	i64 7879037620440914030, ; 118: Xamarin.Android.Support.v7.AppCompat.dll => 0x6d57f6f88a51d86e => 55
	i64 7927939710195668715, ; 119: SkiaSharp.Views.Android.dll => 0x6e05b32992ed16eb => 16
	i64 8044118961405839122, ; 120: System.ComponentModel.Composition => 0x6fa2739369944712 => 119
	i64 8064050204834738623, ; 121: System.Collections.dll => 0x6fe942efa61731bf => 125
	i64 8083354569033831015, ; 122: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 82
	i64 8101777744205214367, ; 123: Xamarin.Android.Support.Annotations => 0x706f4beeec84729f => 33
	i64 8103644804370223335, ; 124: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 115
	i64 8132393369586336849, ; 125: Plugin.InputKit => 0x70dc10aeafef8451 => 12
	i64 8167236081217502503, ; 126: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 4
	i64 8185542183669246576, ; 127: System.Collections => 0x7198e33f4794aa70 => 125
	i64 8187102936927221770, ; 128: SkiaSharp.Views.Forms => 0x719e6ebe771ab80a => 17
	i64 8196541262927413903, ; 129: Xamarin.Android.Support.Interpolator => 0x71bff6d9fb9ec28f => 46
	i64 8385935383968044654, ; 130: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0x7460d3cd16cb566e => 30
	i64 8601935802264776013, ; 131: Xamarin.AndroidX.Transition.dll => 0x7760370982b4ed4d => 96
	i64 8626175481042262068, ; 132: Java.Interop => 0x77b654e585b55834 => 4
	i64 8684531736582871431, ; 133: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 118
	i64 8808820144457481518, ; 134: Xamarin.Android.Support.Loader.dll => 0x7a3f374010b17d2e => 47
	i64 8917102979740339192, ; 135: Xamarin.Android.Support.DocumentFile.dll => 0x7bbfe9ea4d000bf8 => 43
	i64 9324707631942237306, ; 136: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 64
	i64 9475595603812259686, ; 137: Xamarin.Android.Support.Design => 0x838013ff707b9766 => 42
	i64 9662334977499516867, ; 138: System.Numerics.dll => 0x8617827802b0cfc3 => 20
	i64 9678050649315576968, ; 139: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 71
	i64 9711637524876806384, ; 140: Xamarin.AndroidX.Media.dll => 0x86c6aadfd9a2c8f0 => 89
	i64 9808709177481450983, ; 141: Mono.Android.dll => 0x881f890734e555e7 => 8
	i64 9834056768316610435, ; 142: System.Transactions.dll => 0x8879968718899783 => 114
	i64 9866412715007501892, ; 143: Xamarin.Android.Arch.Lifecycle.Common.dll => 0x88ec8a16fd6b6644 => 27
	i64 9998632235833408227, ; 144: Mono.Security => 0x8ac2470b209ebae3 => 121
	i64 10038780035334861115, ; 145: System.Net.Http.dll => 0x8b50e941206af13b => 1
	i64 10134541937470629039, ; 146: yarn_brokerage.Android.dll => 0x8ca52032703c64af => 0
	i64 10229024438826829339, ; 147: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 74
	i64 10303855825347935641, ; 148: Xamarin.Android.Support.Loader => 0x8efea647eeb3fd99 => 47
	i64 10325666697786115060, ; 149: Xamarin.Forms.Extended.InfiniteScrolling.dll => 0x8f4c2327669f43f4 => 103
	i64 10363495123250631811, ; 150: Xamarin.Android.Support.Collections.dll => 0x8fd287e80cd8d483 => 35
	i64 10430153318873392755, ; 151: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 72
	i64 10635644668885628703, ; 152: Xamarin.Android.Support.DrawerLayout.dll => 0x93996679ee34771f => 44
	i64 10847732767863316357, ; 153: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 65
	i64 10850923258212604222, ; 154: Xamarin.Android.Arch.Lifecycle.Runtime => 0x9696393672c9593e => 30
	i64 10984620054693331049, ; 155: Plugin.InputKit.dll => 0x987135bda0a0c069 => 12
	i64 11023048688141570732, ; 156: System.Core => 0x98f9bc61168392ac => 18
	i64 11037814507248023548, ; 157: System.Xml => 0x992e31d0412bf7fc => 23
	i64 11162124722117608902, ; 158: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 100
	i64 11340910727871153756, ; 159: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 73
	i64 11376461258732682436, ; 160: Xamarin.Android.Support.Compat => 0x9de14f3d5fc13cc4 => 36
	i64 11392833485892708388, ; 161: Xamarin.AndroidX.Print.dll => 0x9e1b79b18fcf6824 => 91
	i64 11395105072750394936, ; 162: Xamarin.Android.Support.v7.CardView => 0x9e238bb09789fe38 => 56
	i64 11529969570048099689, ; 163: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 100
	i64 11578238080964724296, ; 164: Xamarin.AndroidX.Legacy.Support.V4 => 0xa0ae2a30c4cd8648 => 81
	i64 11580057168383206117, ; 165: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 62
	i64 11597940890313164233, ; 166: netstandard => 0xa0f429ca8d1805c9 => 112
	i64 11672361001936329215, ; 167: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 78
	i64 11743665907891708234, ; 168: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 122
	i64 11834399401546345650, ; 169: Xamarin.Android.Support.SlidingPaneLayout.dll => 0xa43c3b8deb43ecb2 => 51
	i64 11865714326292153359, ; 170: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa4ab7c5000e8440f => 29
	i64 12137774235383566651, ; 171: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 98
	i64 12388767885335911387, ; 172: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0xabedbec0d236dbdb => 29
	i64 12414299427252656003, ; 173: Xamarin.Android.Support.Compat.dll => 0xac48738e28bad783 => 36
	i64 12451044538927396471, ; 174: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 77
	i64 12466513435562512481, ; 175: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 87
	i64 12487638416075308985, ; 176: Xamarin.AndroidX.DocumentFile.dll => 0xad4d00fa21b0bfb9 => 75
	i64 12538491095302438457, ; 177: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 69
	i64 12550732019250633519, ; 178: System.IO.Compression => 0xae2d28465e8e1b2f => 117
	i64 12559163541709922900, ; 179: Xamarin.Android.Support.v7.CardView.dll => 0xae4b1cb32ba82254 => 56
	i64 12700543734426720211, ; 180: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 70
	i64 12952608645614506925, ; 181: Xamarin.Android.Support.Core.Utils => 0xb3c0e8eff48193ad => 39
	i64 12963446364377008305, ; 182: System.Drawing.Common.dll => 0xb3e769c8fd8548b1 => 116
	i64 13358059602087096138, ; 183: Xamarin.Android.Support.Fragment.dll => 0xb9615c6f1ee5af4a => 45
	i64 13370592475155966277, ; 184: System.Runtime.Serialization => 0xb98de304062ea945 => 2
	i64 13401370062847626945, ; 185: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 98
	i64 13403416310143541304, ; 186: Microcharts.Droid => 0xba02801ea6c86038 => 6
	i64 13491513212026656886, ; 187: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 66
	i64 13492263892638604996, ; 188: SkiaSharp.Views.Forms.dll => 0xbb3e2686788d9ec4 => 17
	i64 13572454107664307259, ; 189: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 92
	i64 13647894001087880694, ; 190: System.Data.dll => 0xbd670f48cb071df6 => 113
	i64 13959074834287824816, ; 191: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 77
	i64 13967638549803255703, ; 192: Xamarin.Forms.Platform.Android => 0xc1d70541e0134797 => 104
	i64 14020090542213545754, ; 193: Xamarin.Forms.Extended.InfiniteScrolling => 0xc2915e110773bf1a => 103
	i64 14124974489674258913, ; 194: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 69
	i64 14125464355221830302, ; 195: System.Threading.dll => 0xc407bafdbc707a9e => 126
	i64 14172845254133543601, ; 196: Xamarin.AndroidX.MultiDex => 0xc4b00faaed35f2b1 => 90
	i64 14261073672896646636, ; 197: Xamarin.AndroidX.Print => 0xc5e982f274ae0dec => 91
	i64 14276485911877471601, ; 198: yarn_brokerage => 0xc620444bfa534d71 => 110
	i64 14369828458497533121, ; 199: Xamarin.Android.Support.Vector.Drawable => 0xc76be2d9300b64c1 => 58
	i64 14400856865250966808, ; 200: Xamarin.Android.Support.Core.UI => 0xc7da1f051a877d18 => 38
	i64 14486659737292545672, ; 201: Xamarin.AndroidX.Lifecycle.LiveData => 0xc90af44707469e88 => 84
	i64 14644440854989303794, ; 202: Xamarin.AndroidX.LocalBroadcastManager.dll => 0xcb3b815e37daeff2 => 88
	i64 14661790646341542033, ; 203: Xamarin.Android.Support.SwipeRefreshLayout => 0xcb7924e94e552091 => 52
	i64 14852515768018889994, ; 204: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 73
	i64 14987728460634540364, ; 205: System.IO.Compression.dll => 0xcfff1ba06622494c => 117
	i64 14988210264188246988, ; 206: Xamarin.AndroidX.DocumentFile => 0xd000d1d307cddbcc => 75
	i64 15076659072870671916, ; 207: System.ObjectModel.dll => 0xd13b0d8c1620662c => 124
	i64 15133485256822086103, ; 208: System.Linq.dll => 0xd204f0a9127dd9d7 => 127
	i64 15188640517174936311, ; 209: Xamarin.Android.Arch.Core.Common => 0xd2c8e413d75142f7 => 25
	i64 15246441518555807158, ; 210: Xamarin.Android.Arch.Core.Common.dll => 0xd3963dc832493db6 => 25
	i64 15326820765897713587, ; 211: Xamarin.Android.Arch.Core.Runtime.dll => 0xd4b3ce481769e7b3 => 26
	i64 15370334346939861994, ; 212: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 72
	i64 15418891414777631748, ; 213: Xamarin.Android.Support.Transition => 0xd5fae80c88241404 => 53
	i64 15457813392950723921, ; 214: Xamarin.Android.Support.Media.Compat => 0xd6852f61c31a8551 => 49
	i64 15568534730848034786, ; 215: Xamarin.Android.Support.VersionedParcelable.dll => 0xd80e8bda21875fe2 => 59
	i64 15609085926864131306, ; 216: System.dll => 0xd89e9cf3334914ea => 19
	i64 15777549416145007739, ; 217: Xamarin.AndroidX.SlidingPaneLayout.dll => 0xdaf51d99d77eb47b => 94
	i64 15810740023422282496, ; 218: Xamarin.Forms.Xaml => 0xdb6b08484c22eb00 => 106
	i64 15817206913877585035, ; 219: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 122
	i64 16154507427712707110, ; 220: System => 0xe03056ea4e39aa26 => 19
	i64 16242842420508142678, ; 221: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe16a2b1f8908ac56 => 37
	i64 16324796876805858114, ; 222: SkiaSharp.dll => 0xe28d5444586b6342 => 15
	i64 16565028646146589191, ; 223: System.ComponentModel.Composition.dll => 0xe5e2cdc9d3bcc207 => 119
	i64 16767985610513713374, ; 224: Xamarin.Android.Arch.Core.Runtime => 0xe8b3da12798808de => 26
	i64 16822611501064131242, ; 225: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 115
	i64 16833383113903931215, ; 226: mscorlib => 0xe99c30c1484d7f4f => 9
	i64 16895806301542741427, ; 227: Plugin.Permissions => 0xea79f6503d42f5b3 => 13
	i64 16932527889823454152, ; 228: Xamarin.Android.Support.Core.Utils.dll => 0xeafc6c67465253c8 => 39
	i64 17001062948826229159, ; 229: Microcharts.Forms.dll => 0xebefe8ad2cd7a9a7 => 7
	i64 17009591894298689098, ; 230: Xamarin.Android.Support.Animated.Vector.Drawable => 0xec0e35b50a097e4a => 32
	i64 17037200463775726619, ; 231: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xec704b8e0a78fc1b => 80
	i64 17285063141349522879, ; 232: Rg.Plugins.Popup => 0xefe0e158cc55fdbf => 14
	i64 17383232329670771222, ; 233: Xamarin.Android.Support.CustomView.dll => 0xf13da5b41a1cc216 => 41
	i64 17428701562824544279, ; 234: Xamarin.Android.Support.Core.UI.dll => 0xf1df2fbaec73d017 => 38
	i64 17483646997724851973, ; 235: Xamarin.Android.Support.ViewPager => 0xf2a2644fe5b6ef05 => 60
	i64 17524135665394030571, ; 236: Xamarin.Android.Support.Print => 0xf3323c8a739097eb => 50
	i64 17544493274320527064, ; 237: Xamarin.AndroidX.AsyncLayoutInflater => 0xf37a8fada41aded8 => 67
	i64 17666959971718154066, ; 238: Xamarin.Android.Support.CustomView => 0xf52da67d9f4e4752 => 41
	i64 17671790519499593115, ; 239: SkiaSharp.Views.Android => 0xf53ecfd92be3959b => 16
	i64 17685921127322830888, ; 240: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 123
	i64 17704177640604968747, ; 241: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 87
	i64 17710060891934109755, ; 242: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 86
	i64 17760961058993581169, ; 243: Xamarin.Android.Arch.Lifecycle.Common => 0xf67b9bfb46dbac71 => 27
	i64 17841643939744178149, ; 244: Xamarin.Android.Arch.Lifecycle.ViewModel => 0xf79a40a25573dfe5 => 31
	i64 17882897186074144999, ; 245: FormsViewGroup => 0xf82cd03e3ac830e7 => 3
	i64 17892495832318972303, ; 246: Xamarin.Forms.Xaml.dll => 0xf84eea293687918f => 106
	i64 17928294245072900555, ; 247: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 118
	i64 17936749993673010118, ; 248: Xamarin.Android.Support.Design.dll => 0xf8ec231615deabc6 => 42
	i64 17958105683855786126, ; 249: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xf93801f92d25c08e => 28
	i64 18025913125965088385, ; 250: System.Threading => 0xfa28e87b91334681 => 126
	i64 18090425465832348288, ; 251: Xamarin.Android.Support.v7.RecyclerView => 0xfb0e1a1d2e9e1a80 => 57
	i64 18116111925905154859, ; 252: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 66
	i64 18129453464017766560, ; 253: System.ServiceModel.Internals.dll => 0xfb98c1df1ec108a0 => 111
	i64 18301997741680159453, ; 254: Xamarin.Android.Support.CursorAdapter => 0xfdfdc1fa58d8eadd => 40
	i64 18380184030268848184 ; 255: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 99
], align 8
@assembly_image_cache_indices = local_unnamed_addr constant [256 x i32] [
	i32 8, i32 6, i32 70, i32 93, i32 14, i32 94, i32 109, i32 31, ; 0..7
	i32 85, i32 116, i32 79, i32 35, i32 28, i32 76, i32 7, i32 114, ; 8..15
	i32 105, i32 121, i32 107, i32 65, i32 45, i32 13, i32 5, i32 2, ; 16..23
	i32 59, i32 63, i32 80, i32 10, i32 55, i32 64, i32 93, i32 57, ; 24..31
	i32 62, i32 54, i32 86, i32 108, i32 10, i32 90, i32 68, i32 76, ; 32..39
	i32 120, i32 83, i32 71, i32 97, i32 22, i32 61, i32 23, i32 9, ; 40..47
	i32 109, i32 11, i32 51, i32 102, i32 44, i32 107, i32 49, i32 5, ; 48..55
	i32 82, i32 21, i32 95, i32 20, i32 22, i32 11, i32 112, i32 92, ; 56..63
	i32 110, i32 108, i32 60, i32 53, i32 43, i32 34, i32 111, i32 99, ; 64..71
	i32 67, i32 61, i32 95, i32 37, i32 48, i32 104, i32 50, i32 101, ; 72..79
	i32 88, i32 46, i32 89, i32 97, i32 96, i32 32, i32 123, i32 18, ; 80..87
	i32 33, i32 84, i32 52, i32 105, i32 3, i32 48, i32 85, i32 83, ; 88..95
	i32 58, i32 68, i32 74, i32 54, i32 120, i32 79, i32 15, i32 24, ; 96..103
	i32 21, i32 0, i32 81, i32 78, i32 40, i32 127, i32 24, i32 124, ; 104..111
	i32 102, i32 101, i32 1, i32 113, i32 34, i32 63, i32 55, i32 16, ; 112..119
	i32 119, i32 125, i32 82, i32 33, i32 115, i32 12, i32 4, i32 125, ; 120..127
	i32 17, i32 46, i32 30, i32 96, i32 4, i32 118, i32 47, i32 43, ; 128..135
	i32 64, i32 42, i32 20, i32 71, i32 89, i32 8, i32 114, i32 27, ; 136..143
	i32 121, i32 1, i32 0, i32 74, i32 47, i32 103, i32 35, i32 72, ; 144..151
	i32 44, i32 65, i32 30, i32 12, i32 18, i32 23, i32 100, i32 73, ; 152..159
	i32 36, i32 91, i32 56, i32 100, i32 81, i32 62, i32 112, i32 78, ; 160..167
	i32 122, i32 51, i32 29, i32 98, i32 29, i32 36, i32 77, i32 87, ; 168..175
	i32 75, i32 69, i32 117, i32 56, i32 70, i32 39, i32 116, i32 45, ; 176..183
	i32 2, i32 98, i32 6, i32 66, i32 17, i32 92, i32 113, i32 77, ; 184..191
	i32 104, i32 103, i32 69, i32 126, i32 90, i32 91, i32 110, i32 58, ; 192..199
	i32 38, i32 84, i32 88, i32 52, i32 73, i32 117, i32 75, i32 124, ; 200..207
	i32 127, i32 25, i32 25, i32 26, i32 72, i32 53, i32 49, i32 59, ; 208..215
	i32 19, i32 94, i32 106, i32 122, i32 19, i32 37, i32 15, i32 119, ; 216..223
	i32 26, i32 115, i32 9, i32 13, i32 39, i32 7, i32 32, i32 80, ; 224..231
	i32 14, i32 41, i32 38, i32 60, i32 50, i32 67, i32 41, i32 16, ; 232..239
	i32 123, i32 87, i32 86, i32 27, i32 31, i32 3, i32 106, i32 118, ; 240..247
	i32 42, i32 28, i32 126, i32 57, i32 66, i32 111, i32 40, i32 99 ; 256..255
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

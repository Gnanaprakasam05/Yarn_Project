; ModuleID = 'obj\Debug\110\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Debug\110\android\marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


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
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [256 x i32] [
	i32 32687329, ; 0: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 85
	i32 39109920, ; 1: Newtonsoft.Json.dll => 0x254c520 => 10
	i32 39852199, ; 2: Plugin.Permissions => 0x26018a7 => 13
	i32 57263871, ; 3: Xamarin.Forms.Core.dll => 0x369c6ff => 102
	i32 57967248, ; 4: Xamarin.Android.Support.VersionedParcelable.dll => 0x3748290 => 59
	i32 59463509, ; 5: Xamarin.Forms.Extended.InfiniteScrolling.dll => 0x38b5755 => 103
	i32 86250823, ; 6: XamEffects => 0x5241547 => 108
	i32 101534019, ; 7: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 94
	i32 120558881, ; 8: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 94
	i32 160529393, ; 9: Xamarin.Android.Arch.Core.Common => 0x9917bf1 => 25
	i32 165246403, ; 10: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 70
	i32 166922606, ; 11: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 36
	i32 182336117, ; 12: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 95
	i32 201930040, ; 13: Xamarin.Android.Arch.Core.Runtime => 0xc093538 => 26
	i32 209399409, ; 14: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 68
	i32 219130465, ; 15: Xamarin.Android.Support.v4 => 0xd0faa61 => 54
	i32 220171995, ; 16: System.Diagnostics.Debug => 0xd1f8edb => 123
	i32 230216969, ; 17: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 80
	i32 232815796, ; 18: System.Web.Services => 0xde07cb4 => 120
	i32 278686392, ; 19: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 84
	i32 280482487, ; 20: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 78
	i32 293914992, ; 21: Xamarin.Android.Support.Transition => 0x1184c970 => 53
	i32 318968648, ; 22: Xamarin.AndroidX.Activity.dll => 0x13031348 => 61
	i32 321597661, ; 23: System.Numerics => 0x132b30dd => 20
	i32 342366114, ; 24: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 82
	i32 388313361, ; 25: Xamarin.Android.Support.Animated.Vector.Drawable => 0x17253111 => 32
	i32 389971796, ; 26: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 38
	i32 442521989, ; 27: Xamarin.Essentials => 0x1a605985 => 101
	i32 442565967, ; 28: System.Collections => 0x1a61054f => 125
	i32 450948140, ; 29: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 77
	i32 465846621, ; 30: mscorlib => 0x1bc4415d => 9
	i32 469710990, ; 31: System.dll => 0x1bff388e => 19
	i32 476646585, ; 32: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 78
	i32 486930444, ; 33: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 88
	i32 498788369, ; 34: System.ObjectModel => 0x1dbae811 => 124
	i32 514659665, ; 35: Xamarin.Android.Support.Compat => 0x1ead1551 => 36
	i32 524864063, ; 36: Xamarin.Android.Support.Print => 0x1f48ca3f => 50
	i32 525008092, ; 37: SkiaSharp.dll => 0x1f4afcdc => 15
	i32 526420162, ; 38: System.Transactions.dll => 0x1f6088c2 => 114
	i32 539750087, ; 39: Xamarin.Android.Support.Design => 0x202beec7 => 42
	i32 571524804, ; 40: Xamarin.Android.Support.v7.RecyclerView => 0x2210c6c4 => 57
	i32 605376203, ; 41: System.IO.Compression.FileSystem => 0x24154ecb => 118
	i32 627609679, ; 42: Xamarin.AndroidX.CustomView => 0x2568904f => 74
	i32 650358235, ; 43: yarn_brokerage.dll => 0x26c3addb => 110
	i32 656518820, ; 44: yarn_brokerage.Android.dll => 0x2721aea4 => 0
	i32 663517072, ; 45: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 99
	i32 666292255, ; 46: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 65
	i32 690569205, ; 47: System.Xml.Linq.dll => 0x29293ff5 => 24
	i32 692692150, ; 48: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 33
	i32 775507847, ; 49: System.IO.Compression => 0x2e394f87 => 117
	i32 791272004, ; 50: Plugin.InputKit => 0x2f29da44 => 12
	i32 801787702, ; 51: Xamarin.Android.Support.Interpolator => 0x2fca4f36 => 46
	i32 809851609, ; 52: System.Drawing.Common.dll => 0x30455ad9 => 116
	i32 843511501, ; 53: Xamarin.AndroidX.Print => 0x3246f6cd => 91
	i32 882883187, ; 54: Xamarin.Android.Support.v4.dll => 0x349fba73 => 54
	i32 886248193, ; 55: Microcharts.Droid => 0x34d31301 => 6
	i32 902159924, ; 56: Rg.Plugins.Popup => 0x35c5de34 => 14
	i32 916714535, ; 57: Xamarin.Android.Support.Print.dll => 0x36a3f427 => 50
	i32 955402788, ; 58: Newtonsoft.Json => 0x38f24a24 => 10
	i32 957807352, ; 59: Plugin.CurrentActivity => 0x3916faf8 => 11
	i32 958213972, ; 60: Xamarin.Android.Support.Media.Compat => 0x391d2f54 => 49
	i32 967690846, ; 61: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 82
	i32 974778368, ; 62: FormsViewGroup.dll => 0x3a19f000 => 3
	i32 987342438, ; 63: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0x3ad9a666 => 29
	i32 992768348, ; 64: System.Collections.dll => 0x3b2c715c => 125
	i32 1012816738, ; 65: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 93
	i32 1035644815, ; 66: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 64
	i32 1042160112, ; 67: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 105
	i32 1052210849, ; 68: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 86
	i32 1082742692, ; 69: XamEffects.Droid => 0x408957a4 => 109
	i32 1098167829, ; 70: Xamarin.Android.Arch.Lifecycle.ViewModel => 0x4174b615 => 31
	i32 1098259244, ; 71: System => 0x41761b2c => 19
	i32 1137654822, ; 72: Plugin.Permissions.dll => 0x43cf3c26 => 13
	i32 1175144683, ; 73: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 97
	i32 1204270330, ; 74: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 65
	i32 1267360935, ; 75: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 98
	i32 1292763917, ; 76: Xamarin.Android.Support.CursorAdapter.dll => 0x4d0e030d => 40
	i32 1293217323, ; 77: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 76
	i32 1297413738, ; 78: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0x4d54f66a => 28
	i32 1324164729, ; 79: System.Linq => 0x4eed2679 => 127
	i32 1359785034, ; 80: Xamarin.Android.Support.Design.dll => 0x510cac4a => 42
	i32 1365406463, ; 81: System.ServiceModel.Internals.dll => 0x516272ff => 111
	i32 1376866003, ; 82: Xamarin.AndroidX.SavedState => 0x52114ed3 => 93
	i32 1395857551, ; 83: Xamarin.AndroidX.Media.dll => 0x5333188f => 89
	i32 1406073936, ; 84: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 71
	i32 1445445088, ; 85: Xamarin.Android.Support.Fragment => 0x5627bde0 => 45
	i32 1460219004, ; 86: Xamarin.Forms.Xaml => 0x57092c7c => 106
	i32 1462112819, ; 87: System.IO.Compression.dll => 0x57261233 => 117
	i32 1469204771, ; 88: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 63
	i32 1574652163, ; 89: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 39
	i32 1582372066, ; 90: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 75
	i32 1582526884, ; 91: Microcharts.Forms.dll => 0x5e5371a4 => 7
	i32 1587447679, ; 92: Xamarin.Android.Arch.Core.Common.dll => 0x5e9e877f => 25
	i32 1592978981, ; 93: System.Runtime.Serialization.dll => 0x5ef2ee25 => 2
	i32 1622152042, ; 94: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 87
	i32 1636350590, ; 95: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 73
	i32 1639515021, ; 96: System.Net.Http.dll => 0x61b9038d => 1
	i32 1657153582, ; 97: System.Runtime => 0x62c6282e => 22
	i32 1658251792, ; 98: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 107
	i32 1662014763, ; 99: Xamarin.Android.Support.Vector.Drawable => 0x6310552b => 58
	i32 1701541528, ; 100: System.Diagnostics.Debug.dll => 0x656b7698 => 123
	i32 1722051300, ; 101: SkiaSharp.Views.Forms => 0x66a46ae4 => 17
	i32 1729485958, ; 102: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 69
	i32 1766324549, ; 103: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 95
	i32 1776026572, ; 104: System.Core.dll => 0x69dc03cc => 18
	i32 1788241197, ; 105: Xamarin.AndroidX.Fragment => 0x6a96652d => 77
	i32 1808609942, ; 106: Xamarin.AndroidX.Loader => 0x6bcd3296 => 87
	i32 1813201214, ; 107: Xamarin.Google.Android.Material => 0x6c13413e => 107
	i32 1866717392, ; 108: Xamarin.Android.Support.Interpolator.dll => 0x6f43d8d0 => 46
	i32 1867746548, ; 109: Xamarin.Essentials.dll => 0x6f538cf4 => 101
	i32 1877418711, ; 110: Xamarin.Android.Support.v7.RecyclerView.dll => 0x6fe722d7 => 57
	i32 1878053835, ; 111: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 106
	i32 1885316902, ; 112: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 66
	i32 1916660109, ; 113: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x723de98d => 31
	i32 1919157823, ; 114: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 90
	i32 2019465201, ; 115: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 86
	i32 2030069428, ; 116: XamEffects.dll => 0x790066b4 => 108
	i32 2037417872, ; 117: Xamarin.Android.Support.ViewPager => 0x79708790 => 60
	i32 2044222327, ; 118: Xamarin.Android.Support.Loader => 0x79d85b77 => 47
	i32 2055257422, ; 119: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 83
	i32 2079903147, ; 120: System.Runtime.dll => 0x7bf8cdab => 22
	i32 2090596640, ; 121: System.Numerics.Vectors => 0x7c9bf920 => 21
	i32 2097448633, ; 122: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 79
	i32 2126786730, ; 123: Xamarin.Forms.Platform.Android => 0x7ec430aa => 104
	i32 2139458754, ; 124: Xamarin.Android.Support.DrawerLayout => 0x7f858cc2 => 44
	i32 2166116741, ; 125: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 39
	i32 2173482090, ; 126: Xamarin.Forms.Extended.InfiniteScrolling => 0x818cb46a => 103
	i32 2193016926, ; 127: System.ObjectModel.dll => 0x82b6c85e => 124
	i32 2196165013, ; 128: Xamarin.Android.Support.VersionedParcelable => 0x82e6d195 => 59
	i32 2201231467, ; 129: System.Net.Http => 0x8334206b => 1
	i32 2202858264, ; 130: XamEffects.Droid.dll => 0x834cf318 => 109
	i32 2217644978, ; 131: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 97
	i32 2244775296, ; 132: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 88
	i32 2256548716, ; 133: Xamarin.AndroidX.MultiDex => 0x8680336c => 90
	i32 2261435625, ; 134: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 81
	i32 2279755925, ; 135: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 92
	i32 2315684594, ; 136: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 62
	i32 2330457430, ; 137: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 38
	i32 2330986192, ; 138: Xamarin.Android.Support.SlidingPaneLayout.dll => 0x8af006d0 => 51
	i32 2373288475, ; 139: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 45
	i32 2440966767, ; 140: Xamarin.Android.Support.Loader.dll => 0x917e326f => 47
	i32 2471841756, ; 141: netstandard.dll => 0x93554fdc => 112
	i32 2475788418, ; 142: Java.Interop.dll => 0x93918882 => 4
	i32 2487632829, ; 143: Xamarin.Android.Support.DocumentFile => 0x944643bd => 43
	i32 2501346920, ; 144: System.Data.DataSetExtensions => 0x95178668 => 115
	i32 2505896520, ; 145: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 85
	i32 2581819634, ; 146: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 98
	i32 2620871830, ; 147: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 73
	i32 2633051222, ; 148: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 84
	i32 2698266930, ; 149: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa0d44932 => 29
	i32 2715334215, ; 150: System.Threading.Tasks.dll => 0xa1d8b647 => 122
	i32 2732626843, ; 151: Xamarin.AndroidX.Activity => 0xa2e0939b => 61
	i32 2737747696, ; 152: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 63
	i32 2751899777, ; 153: Xamarin.Android.Support.Collections => 0xa406a881 => 35
	i32 2766581644, ; 154: Xamarin.Forms.Core => 0xa4e6af8c => 102
	i32 2778768386, ; 155: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 100
	i32 2788639665, ; 156: Xamarin.Android.Support.LocalBroadcastManager => 0xa63743b1 => 48
	i32 2788775637, ; 157: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0xa63956d5 => 52
	i32 2795602088, ; 158: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 16
	i32 2806986428, ; 159: Plugin.CurrentActivity.dll => 0xa74f36bc => 11
	i32 2810250172, ; 160: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 71
	i32 2819470561, ; 161: System.Xml.dll => 0xa80db4e1 => 23
	i32 2853208004, ; 162: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 100
	i32 2855708567, ; 163: Xamarin.AndroidX.Transition => 0xaa36a797 => 96
	i32 2861816565, ; 164: Rg.Plugins.Popup.dll => 0xaa93daf5 => 14
	i32 2876493027, ; 165: Xamarin.Android.Support.SwipeRefreshLayout => 0xab73cce3 => 52
	i32 2893257763, ; 166: Xamarin.Android.Arch.Core.Runtime.dll => 0xac739c23 => 26
	i32 2903344695, ; 167: System.ComponentModel.Composition => 0xad0d8637 => 119
	i32 2903680942, ; 168: yarn_brokerage => 0xad12a7ae => 110
	i32 2905242038, ; 169: mscorlib.dll => 0xad2a79b6 => 9
	i32 2912489636, ; 170: SkiaSharp.Views.Android => 0xad9910a4 => 16
	i32 2919462931, ; 171: System.Numerics.Vectors.dll => 0xae037813 => 21
	i32 2921692953, ; 172: Xamarin.Android.Support.CustomView.dll => 0xae257f19 => 41
	i32 2922925221, ; 173: Xamarin.Android.Support.Vector.Drawable.dll => 0xae384ca5 => 58
	i32 2974793899, ; 174: SkiaSharp.Views.Forms.dll => 0xb14fc0ab => 17
	i32 2978675010, ; 175: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 76
	i32 3024354802, ; 176: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 80
	i32 3036068679, ; 177: Microcharts.Droid.dll => 0xb4f6bb47 => 6
	i32 3044182254, ; 178: FormsViewGroup => 0xb57288ee => 3
	i32 3056250942, ; 179: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0xb62ab03e => 34
	i32 3068715062, ; 180: Xamarin.Android.Arch.Lifecycle.Common => 0xb6e8e036 => 27
	i32 3075834255, ; 181: System.Threading.Tasks => 0xb755818f => 122
	i32 3092211740, ; 182: Xamarin.Android.Support.Media.Compat.dll => 0xb84f681c => 49
	i32 3111772706, ; 183: System.Runtime.Serialization => 0xb979e222 => 2
	i32 3204380047, ; 184: System.Data.dll => 0xbefef58f => 113
	i32 3204912593, ; 185: Xamarin.Android.Support.AsyncLayoutInflater => 0xbf0715d1 => 34
	i32 3211777861, ; 186: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 75
	i32 3220365878, ; 187: System.Threading => 0xbff2e236 => 126
	i32 3233339011, ; 188: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xc0b8d683 => 28
	i32 3247949154, ; 189: Mono.Security => 0xc197c562 => 121
	i32 3258312781, ; 190: Xamarin.AndroidX.CardView => 0xc235e84d => 69
	i32 3267021929, ; 191: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 67
	i32 3296380511, ; 192: Xamarin.Android.Support.Collections.dll => 0xc47ac65f => 35
	i32 3317135071, ; 193: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 74
	i32 3317144872, ; 194: System.Data => 0xc5b79d28 => 113
	i32 3321585405, ; 195: Xamarin.Android.Support.DocumentFile.dll => 0xc5fb5efd => 43
	i32 3340387945, ; 196: SkiaSharp => 0xc71a4669 => 15
	i32 3340431453, ; 197: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 66
	i32 3352662376, ; 198: Xamarin.Android.Support.CoordinaterLayout => 0xc7d59168 => 37
	i32 3353484488, ; 199: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 79
	i32 3357663996, ; 200: Xamarin.Android.Support.CursorAdapter => 0xc821e2fc => 40
	i32 3362522851, ; 201: Xamarin.AndroidX.Core => 0xc86c06e3 => 72
	i32 3366347497, ; 202: Java.Interop => 0xc8a662e9 => 4
	i32 3374999561, ; 203: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 92
	i32 3404865022, ; 204: System.ServiceModel.Internals => 0xcaf21dfe => 111
	i32 3429136800, ; 205: System.Xml => 0xcc6479a0 => 23
	i32 3430777524, ; 206: netstandard => 0xcc7d82b4 => 112
	i32 3439690031, ; 207: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 33
	i32 3450581182, ; 208: yarn_brokerage.Android => 0xcdabb0be => 0
	i32 3455791806, ; 209: Microcharts => 0xcdfb32be => 5
	i32 3476120550, ; 210: Mono.Android => 0xcf3163e6 => 8
	i32 3486566296, ; 211: System.Transactions => 0xcfd0c798 => 114
	i32 3498942916, ; 212: Xamarin.Android.Support.v7.CardView.dll => 0xd08da1c4 => 56
	i32 3501239056, ; 213: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 67
	i32 3509114376, ; 214: System.Xml.Linq => 0xd128d608 => 24
	i32 3536029504, ; 215: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 104
	i32 3547625832, ; 216: Xamarin.Android.Support.SlidingPaneLayout => 0xd3747968 => 51
	i32 3567349600, ; 217: System.ComponentModel.Composition.dll => 0xd4a16f60 => 119
	i32 3608519521, ; 218: System.Linq.dll => 0xd715a361 => 127
	i32 3627220390, ; 219: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 91
	i32 3632359727, ; 220: Xamarin.Forms.Platform => 0xd881692f => 105
	i32 3641597786, ; 221: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 83
	i32 3664423555, ; 222: Xamarin.Android.Support.ViewPager.dll => 0xda6aaa83 => 60
	i32 3668042751, ; 223: Microcharts.dll => 0xdaa1e3ff => 5
	i32 3672681054, ; 224: Mono.Android.dll => 0xdae8aa5e => 8
	i32 3676310014, ; 225: System.Web.Services.dll => 0xdb2009fe => 120
	i32 3678221644, ; 226: Xamarin.Android.Support.v7.AppCompat => 0xdb3d354c => 55
	i32 3681174138, ; 227: Xamarin.Android.Arch.Lifecycle.Common.dll => 0xdb6a427a => 27
	i32 3682565725, ; 228: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 68
	i32 3689375977, ; 229: System.Drawing.Common => 0xdbe768e9 => 116
	i32 3714038699, ; 230: Xamarin.Android.Support.LocalBroadcastManager.dll => 0xdd5fbbab => 48
	i32 3718463572, ; 231: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0xdda34054 => 32
	i32 3718780102, ; 232: Xamarin.AndroidX.Annotation => 0xdda814c6 => 62
	i32 3758932259, ; 233: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 81
	i32 3776062606, ; 234: Xamarin.Android.Support.DrawerLayout.dll => 0xe112248e => 44
	i32 3776811843, ; 235: Plugin.InputKit.dll => 0xe11d9343 => 12
	i32 3786282454, ; 236: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 70
	i32 3822602673, ; 237: Xamarin.AndroidX.Media => 0xe3d849b1 => 89
	i32 3829621856, ; 238: System.Numerics.dll => 0xe4436460 => 20
	i32 3832731800, ; 239: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe472d898 => 37
	i32 3862817207, ; 240: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0xe63de9b7 => 30
	i32 3874897629, ; 241: Xamarin.Android.Arch.Lifecycle.Runtime => 0xe6f63edd => 30
	i32 3883175360, ; 242: Xamarin.Android.Support.v7.AppCompat.dll => 0xe7748dc0 => 55
	i32 3885922214, ; 243: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 96
	i32 3896760992, ; 244: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 72
	i32 3903721208, ; 245: Microcharts.Forms => 0xe8ae0ef8 => 7
	i32 3920810846, ; 246: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 118
	i32 3921031405, ; 247: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 99
	i32 3945713374, ; 248: System.Data.DataSetExtensions.dll => 0xeb2ecede => 115
	i32 3955647286, ; 249: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 64
	i32 4063435586, ; 250: Xamarin.Android.Support.CustomView => 0xf2331b42 => 41
	i32 4073602200, ; 251: System.Threading.dll => 0xf2ce3c98 => 126
	i32 4105002889, ; 252: Mono.Security.dll => 0xf4ad5f89 => 121
	i32 4151237749, ; 253: System.Core => 0xf76edc75 => 18
	i32 4216993138, ; 254: Xamarin.Android.Support.Transition.dll => 0xfb5a3572 => 53
	i32 4219003402 ; 255: Xamarin.Android.Support.v7.CardView => 0xfb78e20a => 56
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [256 x i32] [
	i32 85, i32 10, i32 13, i32 102, i32 59, i32 103, i32 108, i32 94, ; 0..7
	i32 94, i32 25, i32 70, i32 36, i32 95, i32 26, i32 68, i32 54, ; 8..15
	i32 123, i32 80, i32 120, i32 84, i32 78, i32 53, i32 61, i32 20, ; 16..23
	i32 82, i32 32, i32 38, i32 101, i32 125, i32 77, i32 9, i32 19, ; 24..31
	i32 78, i32 88, i32 124, i32 36, i32 50, i32 15, i32 114, i32 42, ; 32..39
	i32 57, i32 118, i32 74, i32 110, i32 0, i32 99, i32 65, i32 24, ; 40..47
	i32 33, i32 117, i32 12, i32 46, i32 116, i32 91, i32 54, i32 6, ; 48..55
	i32 14, i32 50, i32 10, i32 11, i32 49, i32 82, i32 3, i32 29, ; 56..63
	i32 125, i32 93, i32 64, i32 105, i32 86, i32 109, i32 31, i32 19, ; 64..71
	i32 13, i32 97, i32 65, i32 98, i32 40, i32 76, i32 28, i32 127, ; 72..79
	i32 42, i32 111, i32 93, i32 89, i32 71, i32 45, i32 106, i32 117, ; 80..87
	i32 63, i32 39, i32 75, i32 7, i32 25, i32 2, i32 87, i32 73, ; 88..95
	i32 1, i32 22, i32 107, i32 58, i32 123, i32 17, i32 69, i32 95, ; 96..103
	i32 18, i32 77, i32 87, i32 107, i32 46, i32 101, i32 57, i32 106, ; 104..111
	i32 66, i32 31, i32 90, i32 86, i32 108, i32 60, i32 47, i32 83, ; 112..119
	i32 22, i32 21, i32 79, i32 104, i32 44, i32 39, i32 103, i32 124, ; 120..127
	i32 59, i32 1, i32 109, i32 97, i32 88, i32 90, i32 81, i32 92, ; 128..135
	i32 62, i32 38, i32 51, i32 45, i32 47, i32 112, i32 4, i32 43, ; 136..143
	i32 115, i32 85, i32 98, i32 73, i32 84, i32 29, i32 122, i32 61, ; 144..151
	i32 63, i32 35, i32 102, i32 100, i32 48, i32 52, i32 16, i32 11, ; 152..159
	i32 71, i32 23, i32 100, i32 96, i32 14, i32 52, i32 26, i32 119, ; 160..167
	i32 110, i32 9, i32 16, i32 21, i32 41, i32 58, i32 17, i32 76, ; 168..175
	i32 80, i32 6, i32 3, i32 34, i32 27, i32 122, i32 49, i32 2, ; 176..183
	i32 113, i32 34, i32 75, i32 126, i32 28, i32 121, i32 69, i32 67, ; 184..191
	i32 35, i32 74, i32 113, i32 43, i32 15, i32 66, i32 37, i32 79, ; 192..199
	i32 40, i32 72, i32 4, i32 92, i32 111, i32 23, i32 112, i32 33, ; 200..207
	i32 0, i32 5, i32 8, i32 114, i32 56, i32 67, i32 24, i32 104, ; 208..215
	i32 51, i32 119, i32 127, i32 91, i32 105, i32 83, i32 60, i32 5, ; 216..223
	i32 8, i32 120, i32 55, i32 27, i32 68, i32 116, i32 48, i32 32, ; 224..231
	i32 62, i32 81, i32 44, i32 12, i32 70, i32 89, i32 20, i32 37, ; 232..239
	i32 30, i32 30, i32 55, i32 96, i32 72, i32 7, i32 118, i32 99, ; 240..247
	i32 115, i32 64, i32 41, i32 126, i32 121, i32 18, i32 53, i32 56 ; 256..255
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ a8a26c7b003e2524cc98acb2c2ffc2ddea0f6692"}
!llvm.linker.options = !{}

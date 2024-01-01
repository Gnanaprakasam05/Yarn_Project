; ModuleID = 'obj\Release\110\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Release\110\android\marshal_methods.armeabi-v7a.ll"
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
@assembly_image_cache_hashes = local_unnamed_addr constant [108 x i32] [
	i32 39109920, ; 0: Newtonsoft.Json.dll => 0x254c520 => 34
	i32 39852199, ; 1: Plugin.Permissions => 0x26018a7 => 37
	i32 57263871, ; 2: Xamarin.Forms.Core.dll => 0x369c6ff => 45
	i32 59463509, ; 3: Xamarin.Forms.Extended.InfiniteScrolling.dll => 0x38b5755 => 46
	i32 86250823, ; 4: XamEffects => 0x5241547 => 50
	i32 182336117, ; 5: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 23
	i32 318968648, ; 6: Xamarin.AndroidX.Activity.dll => 0x13031348 => 42
	i32 321597661, ; 7: System.Numerics => 0x132b30dd => 7
	i32 342366114, ; 8: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 18
	i32 442521989, ; 9: Xamarin.Essentials => 0x1a605985 => 44
	i32 450948140, ; 10: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 16
	i32 465846621, ; 11: mscorlib => 0x1bc4415d => 4
	i32 469710990, ; 12: System.dll => 0x1bff388e => 6
	i32 525008092, ; 13: SkiaSharp.dll => 0x1f4afcdc => 39
	i32 650358235, ; 14: yarn_brokerage.dll => 0x26c3addb => 53
	i32 656518820, ; 15: yarn_brokerage.Android.dll => 0x2721aea4 => 52
	i32 690569205, ; 16: System.Xml.Linq.dll => 0x29293ff5 => 9
	i32 791272004, ; 17: Plugin.InputKit => 0x2f29da44 => 36
	i32 809851609, ; 18: System.Drawing.Common.dll => 0x30455ad9 => 28
	i32 886248193, ; 19: Microcharts.Droid => 0x34d31301 => 32
	i32 902159924, ; 20: Rg.Plugins.Popup => 0x35c5de34 => 38
	i32 955402788, ; 21: Newtonsoft.Json => 0x38f24a24 => 34
	i32 957807352, ; 22: Plugin.CurrentActivity => 0x3916faf8 => 35
	i32 967690846, ; 23: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 18
	i32 974778368, ; 24: FormsViewGroup.dll => 0x3a19f000 => 30
	i32 1012816738, ; 25: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 43
	i32 1035644815, ; 26: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 11
	i32 1042160112, ; 27: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 48
	i32 1052210849, ; 28: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 20
	i32 1082742692, ; 29: XamEffects.Droid => 0x408957a4 => 51
	i32 1098259244, ; 30: System => 0x41761b2c => 6
	i32 1137654822, ; 31: Plugin.Permissions.dll => 0x43cf3c26 => 37
	i32 1293217323, ; 32: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 15
	i32 1365406463, ; 33: System.ServiceModel.Internals.dll => 0x516272ff => 26
	i32 1376866003, ; 34: Xamarin.AndroidX.SavedState => 0x52114ed3 => 43
	i32 1406073936, ; 35: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 13
	i32 1460219004, ; 36: Xamarin.Forms.Xaml => 0x57092c7c => 49
	i32 1469204771, ; 37: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 10
	i32 1582526884, ; 38: Microcharts.Forms.dll => 0x5e5371a4 => 33
	i32 1592978981, ; 39: System.Runtime.Serialization.dll => 0x5ef2ee25 => 1
	i32 1622152042, ; 40: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 21
	i32 1639515021, ; 41: System.Net.Http.dll => 0x61b9038d => 0
	i32 1658251792, ; 42: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 25
	i32 1722051300, ; 43: SkiaSharp.Views.Forms => 0x66a46ae4 => 41
	i32 1729485958, ; 44: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 12
	i32 1766324549, ; 45: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 23
	i32 1776026572, ; 46: System.Core.dll => 0x69dc03cc => 5
	i32 1788241197, ; 47: Xamarin.AndroidX.Fragment => 0x6a96652d => 16
	i32 1808609942, ; 48: Xamarin.AndroidX.Loader => 0x6bcd3296 => 21
	i32 1813201214, ; 49: Xamarin.Google.Android.Material => 0x6c13413e => 25
	i32 1867746548, ; 50: Xamarin.Essentials.dll => 0x6f538cf4 => 44
	i32 1878053835, ; 51: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 49
	i32 2019465201, ; 52: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 20
	i32 2030069428, ; 53: XamEffects.dll => 0x790066b4 => 50
	i32 2055257422, ; 54: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 19
	i32 2097448633, ; 55: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 17
	i32 2126786730, ; 56: Xamarin.Forms.Platform.Android => 0x7ec430aa => 47
	i32 2173482090, ; 57: Xamarin.Forms.Extended.InfiniteScrolling => 0x818cb46a => 46
	i32 2201231467, ; 58: System.Net.Http => 0x8334206b => 0
	i32 2202858264, ; 59: XamEffects.Droid.dll => 0x834cf318 => 51
	i32 2279755925, ; 60: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 22
	i32 2475788418, ; 61: Java.Interop.dll => 0x93918882 => 2
	i32 2732626843, ; 62: Xamarin.AndroidX.Activity => 0xa2e0939b => 42
	i32 2737747696, ; 63: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 10
	i32 2766581644, ; 64: Xamarin.Forms.Core => 0xa4e6af8c => 45
	i32 2778768386, ; 65: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 24
	i32 2795602088, ; 66: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 40
	i32 2806986428, ; 67: Plugin.CurrentActivity.dll => 0xa74f36bc => 35
	i32 2810250172, ; 68: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 13
	i32 2819470561, ; 69: System.Xml.dll => 0xa80db4e1 => 8
	i32 2853208004, ; 70: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 24
	i32 2861816565, ; 71: Rg.Plugins.Popup.dll => 0xaa93daf5 => 38
	i32 2903680942, ; 72: yarn_brokerage => 0xad12a7ae => 53
	i32 2905242038, ; 73: mscorlib.dll => 0xad2a79b6 => 4
	i32 2912489636, ; 74: SkiaSharp.Views.Android => 0xad9910a4 => 40
	i32 2974793899, ; 75: SkiaSharp.Views.Forms.dll => 0xb14fc0ab => 41
	i32 2978675010, ; 76: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 15
	i32 3036068679, ; 77: Microcharts.Droid.dll => 0xb4f6bb47 => 32
	i32 3044182254, ; 78: FormsViewGroup => 0xb57288ee => 30
	i32 3111772706, ; 79: System.Runtime.Serialization => 0xb979e222 => 1
	i32 3204380047, ; 80: System.Data.dll => 0xbefef58f => 27
	i32 3247949154, ; 81: Mono.Security => 0xc197c562 => 29
	i32 3258312781, ; 82: Xamarin.AndroidX.CardView => 0xc235e84d => 12
	i32 3317144872, ; 83: System.Data => 0xc5b79d28 => 27
	i32 3340387945, ; 84: SkiaSharp => 0xc71a4669 => 39
	i32 3353484488, ; 85: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 17
	i32 3362522851, ; 86: Xamarin.AndroidX.Core => 0xc86c06e3 => 14
	i32 3366347497, ; 87: Java.Interop => 0xc8a662e9 => 2
	i32 3374999561, ; 88: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 22
	i32 3404865022, ; 89: System.ServiceModel.Internals => 0xcaf21dfe => 26
	i32 3429136800, ; 90: System.Xml => 0xcc6479a0 => 8
	i32 3450581182, ; 91: yarn_brokerage.Android => 0xcdabb0be => 52
	i32 3455791806, ; 92: Microcharts => 0xcdfb32be => 31
	i32 3476120550, ; 93: Mono.Android => 0xcf3163e6 => 3
	i32 3509114376, ; 94: System.Xml.Linq => 0xd128d608 => 9
	i32 3536029504, ; 95: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 47
	i32 3632359727, ; 96: Xamarin.Forms.Platform => 0xd881692f => 48
	i32 3641597786, ; 97: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 19
	i32 3668042751, ; 98: Microcharts.dll => 0xdaa1e3ff => 31
	i32 3672681054, ; 99: Mono.Android.dll => 0xdae8aa5e => 3
	i32 3689375977, ; 100: System.Drawing.Common => 0xdbe768e9 => 28
	i32 3776811843, ; 101: Plugin.InputKit.dll => 0xe11d9343 => 36
	i32 3829621856, ; 102: System.Numerics.dll => 0xe4436460 => 7
	i32 3896760992, ; 103: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 14
	i32 3903721208, ; 104: Microcharts.Forms => 0xe8ae0ef8 => 33
	i32 3955647286, ; 105: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 11
	i32 4105002889, ; 106: Mono.Security.dll => 0xf4ad5f89 => 29
	i32 4151237749 ; 107: System.Core => 0xf76edc75 => 5
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [108 x i32] [
	i32 34, i32 37, i32 45, i32 46, i32 50, i32 23, i32 42, i32 7, ; 0..7
	i32 18, i32 44, i32 16, i32 4, i32 6, i32 39, i32 53, i32 52, ; 8..15
	i32 9, i32 36, i32 28, i32 32, i32 38, i32 34, i32 35, i32 18, ; 16..23
	i32 30, i32 43, i32 11, i32 48, i32 20, i32 51, i32 6, i32 37, ; 24..31
	i32 15, i32 26, i32 43, i32 13, i32 49, i32 10, i32 33, i32 1, ; 32..39
	i32 21, i32 0, i32 25, i32 41, i32 12, i32 23, i32 5, i32 16, ; 40..47
	i32 21, i32 25, i32 44, i32 49, i32 20, i32 50, i32 19, i32 17, ; 48..55
	i32 47, i32 46, i32 0, i32 51, i32 22, i32 2, i32 42, i32 10, ; 56..63
	i32 45, i32 24, i32 40, i32 35, i32 13, i32 8, i32 24, i32 38, ; 64..71
	i32 53, i32 4, i32 40, i32 41, i32 15, i32 32, i32 30, i32 1, ; 72..79
	i32 27, i32 29, i32 12, i32 27, i32 39, i32 17, i32 14, i32 2, ; 80..87
	i32 22, i32 26, i32 8, i32 52, i32 31, i32 3, i32 9, i32 47, ; 88..95
	i32 48, i32 19, i32 31, i32 3, i32 28, i32 36, i32 7, i32 14, ; 96..103
	i32 33, i32 11, i32 29, i32 5 ; 104..107
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

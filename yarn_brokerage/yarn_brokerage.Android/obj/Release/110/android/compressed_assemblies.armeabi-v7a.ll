; ModuleID = 'obj\Release\110\android\compressed_assemblies.armeabi-v7a.ll'
source_filename = "obj\Release\110\android\compressed_assemblies.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.CompressedAssemblyDescriptor = type {
	i32,; uint32_t uncompressed_file_size
	i8,; bool loaded
	i8*; uint8_t* data
}

%struct.CompressedAssemblies = type {
	i32,; uint32_t count
	%struct.CompressedAssemblyDescriptor*; CompressedAssemblyDescriptor* descriptors
}
@__CompressedAssemblyDescriptor_data_0 = internal global [15360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_1 = internal global [166912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_2 = internal global [5632 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_3 = internal global [6144 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_4 = internal global [39936 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_5 = internal global [2356224 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_6 = internal global [121856 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_7 = internal global [682496 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_8 = internal global [9216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_9 = internal global [228352 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_10 = internal global [27648 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_11 = internal global [218112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_12 = internal global [49504 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_13 = internal global [155488 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_14 = internal global [405872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_15 = internal global [392704 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_16 = internal global [747520 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_17 = internal global [34304 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_18 = internal global [218624 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_19 = internal global [38912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_20 = internal global [419328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_21 = internal global [55808 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_22 = internal global [65024 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_23 = internal global [1397760 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_24 = internal global [880640 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_25 = internal global [101888 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_26 = internal global [10240 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_27 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_28 = internal global [14848 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_29 = internal global [453120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_30 = internal global [15872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_31 = internal global [68608 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_32 = internal global [481792 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_33 = internal global [38912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_34 = internal global [146944 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_35 = internal global [14336 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_36 = internal global [14336 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_37 = internal global [14848 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_38 = internal global [8704 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_39 = internal global [34304 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_40 = internal global [389120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_41 = internal global [12800 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_42 = internal global [25600 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_43 = internal global [52224 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_44 = internal global [29184 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_45 = internal global [1191424 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_46 = internal global [12800 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_47 = internal global [798720 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_48 = internal global [132608 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_49 = internal global [102400 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_50 = internal global [169472 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_51 = internal global [2115072 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_52 = internal global [402944 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_53 = internal global [2255360 x i8] zeroinitializer, align 1


; Compressed assembly data storage
@compressed_assembly_descriptors = internal global [54 x %struct.CompressedAssemblyDescriptor] [
	; 0
	%struct.CompressedAssemblyDescriptor {
		i32 15360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15360 x i8], [15360 x i8]* @__CompressedAssemblyDescriptor_data_0, i32 0, i32 0); data
	}, 
	; 1
	%struct.CompressedAssemblyDescriptor {
		i32 166912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([166912 x i8], [166912 x i8]* @__CompressedAssemblyDescriptor_data_1, i32 0, i32 0); data
	}, 
	; 2
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5632 x i8], [5632 x i8]* @__CompressedAssemblyDescriptor_data_2, i32 0, i32 0); data
	}, 
	; 3
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([6144 x i8], [6144 x i8]* @__CompressedAssemblyDescriptor_data_3, i32 0, i32 0); data
	}, 
	; 4
	%struct.CompressedAssemblyDescriptor {
		i32 39936, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([39936 x i8], [39936 x i8]* @__CompressedAssemblyDescriptor_data_4, i32 0, i32 0); data
	}, 
	; 5
	%struct.CompressedAssemblyDescriptor {
		i32 2356224, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2356224 x i8], [2356224 x i8]* @__CompressedAssemblyDescriptor_data_5, i32 0, i32 0); data
	}, 
	; 6
	%struct.CompressedAssemblyDescriptor {
		i32 121856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([121856 x i8], [121856 x i8]* @__CompressedAssemblyDescriptor_data_6, i32 0, i32 0); data
	}, 
	; 7
	%struct.CompressedAssemblyDescriptor {
		i32 682496, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([682496 x i8], [682496 x i8]* @__CompressedAssemblyDescriptor_data_7, i32 0, i32 0); data
	}, 
	; 8
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9216 x i8], [9216 x i8]* @__CompressedAssemblyDescriptor_data_8, i32 0, i32 0); data
	}, 
	; 9
	%struct.CompressedAssemblyDescriptor {
		i32 228352, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([228352 x i8], [228352 x i8]* @__CompressedAssemblyDescriptor_data_9, i32 0, i32 0); data
	}, 
	; 10
	%struct.CompressedAssemblyDescriptor {
		i32 27648, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([27648 x i8], [27648 x i8]* @__CompressedAssemblyDescriptor_data_10, i32 0, i32 0); data
	}, 
	; 11
	%struct.CompressedAssemblyDescriptor {
		i32 218112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([218112 x i8], [218112 x i8]* @__CompressedAssemblyDescriptor_data_11, i32 0, i32 0); data
	}, 
	; 12
	%struct.CompressedAssemblyDescriptor {
		i32 49504, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([49504 x i8], [49504 x i8]* @__CompressedAssemblyDescriptor_data_12, i32 0, i32 0); data
	}, 
	; 13
	%struct.CompressedAssemblyDescriptor {
		i32 155488, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([155488 x i8], [155488 x i8]* @__CompressedAssemblyDescriptor_data_13, i32 0, i32 0); data
	}, 
	; 14
	%struct.CompressedAssemblyDescriptor {
		i32 405872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([405872 x i8], [405872 x i8]* @__CompressedAssemblyDescriptor_data_14, i32 0, i32 0); data
	}, 
	; 15
	%struct.CompressedAssemblyDescriptor {
		i32 392704, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([392704 x i8], [392704 x i8]* @__CompressedAssemblyDescriptor_data_15, i32 0, i32 0); data
	}, 
	; 16
	%struct.CompressedAssemblyDescriptor {
		i32 747520, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([747520 x i8], [747520 x i8]* @__CompressedAssemblyDescriptor_data_16, i32 0, i32 0); data
	}, 
	; 17
	%struct.CompressedAssemblyDescriptor {
		i32 34304, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([34304 x i8], [34304 x i8]* @__CompressedAssemblyDescriptor_data_17, i32 0, i32 0); data
	}, 
	; 18
	%struct.CompressedAssemblyDescriptor {
		i32 218624, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([218624 x i8], [218624 x i8]* @__CompressedAssemblyDescriptor_data_18, i32 0, i32 0); data
	}, 
	; 19
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_19, i32 0, i32 0); data
	}, 
	; 20
	%struct.CompressedAssemblyDescriptor {
		i32 419328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([419328 x i8], [419328 x i8]* @__CompressedAssemblyDescriptor_data_20, i32 0, i32 0); data
	}, 
	; 21
	%struct.CompressedAssemblyDescriptor {
		i32 55808, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([55808 x i8], [55808 x i8]* @__CompressedAssemblyDescriptor_data_21, i32 0, i32 0); data
	}, 
	; 22
	%struct.CompressedAssemblyDescriptor {
		i32 65024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([65024 x i8], [65024 x i8]* @__CompressedAssemblyDescriptor_data_22, i32 0, i32 0); data
	}, 
	; 23
	%struct.CompressedAssemblyDescriptor {
		i32 1397760, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1397760 x i8], [1397760 x i8]* @__CompressedAssemblyDescriptor_data_23, i32 0, i32 0); data
	}, 
	; 24
	%struct.CompressedAssemblyDescriptor {
		i32 880640, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([880640 x i8], [880640 x i8]* @__CompressedAssemblyDescriptor_data_24, i32 0, i32 0); data
	}, 
	; 25
	%struct.CompressedAssemblyDescriptor {
		i32 101888, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([101888 x i8], [101888 x i8]* @__CompressedAssemblyDescriptor_data_25, i32 0, i32 0); data
	}, 
	; 26
	%struct.CompressedAssemblyDescriptor {
		i32 10240, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([10240 x i8], [10240 x i8]* @__CompressedAssemblyDescriptor_data_26, i32 0, i32 0); data
	}, 
	; 27
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_27, i32 0, i32 0); data
	}, 
	; 28
	%struct.CompressedAssemblyDescriptor {
		i32 14848, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14848 x i8], [14848 x i8]* @__CompressedAssemblyDescriptor_data_28, i32 0, i32 0); data
	}, 
	; 29
	%struct.CompressedAssemblyDescriptor {
		i32 453120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([453120 x i8], [453120 x i8]* @__CompressedAssemblyDescriptor_data_29, i32 0, i32 0); data
	}, 
	; 30
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_30, i32 0, i32 0); data
	}, 
	; 31
	%struct.CompressedAssemblyDescriptor {
		i32 68608, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([68608 x i8], [68608 x i8]* @__CompressedAssemblyDescriptor_data_31, i32 0, i32 0); data
	}, 
	; 32
	%struct.CompressedAssemblyDescriptor {
		i32 481792, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([481792 x i8], [481792 x i8]* @__CompressedAssemblyDescriptor_data_32, i32 0, i32 0); data
	}, 
	; 33
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_33, i32 0, i32 0); data
	}, 
	; 34
	%struct.CompressedAssemblyDescriptor {
		i32 146944, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([146944 x i8], [146944 x i8]* @__CompressedAssemblyDescriptor_data_34, i32 0, i32 0); data
	}, 
	; 35
	%struct.CompressedAssemblyDescriptor {
		i32 14336, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14336 x i8], [14336 x i8]* @__CompressedAssemblyDescriptor_data_35, i32 0, i32 0); data
	}, 
	; 36
	%struct.CompressedAssemblyDescriptor {
		i32 14336, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14336 x i8], [14336 x i8]* @__CompressedAssemblyDescriptor_data_36, i32 0, i32 0); data
	}, 
	; 37
	%struct.CompressedAssemblyDescriptor {
		i32 14848, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14848 x i8], [14848 x i8]* @__CompressedAssemblyDescriptor_data_37, i32 0, i32 0); data
	}, 
	; 38
	%struct.CompressedAssemblyDescriptor {
		i32 8704, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([8704 x i8], [8704 x i8]* @__CompressedAssemblyDescriptor_data_38, i32 0, i32 0); data
	}, 
	; 39
	%struct.CompressedAssemblyDescriptor {
		i32 34304, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([34304 x i8], [34304 x i8]* @__CompressedAssemblyDescriptor_data_39, i32 0, i32 0); data
	}, 
	; 40
	%struct.CompressedAssemblyDescriptor {
		i32 389120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([389120 x i8], [389120 x i8]* @__CompressedAssemblyDescriptor_data_40, i32 0, i32 0); data
	}, 
	; 41
	%struct.CompressedAssemblyDescriptor {
		i32 12800, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([12800 x i8], [12800 x i8]* @__CompressedAssemblyDescriptor_data_41, i32 0, i32 0); data
	}, 
	; 42
	%struct.CompressedAssemblyDescriptor {
		i32 25600, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([25600 x i8], [25600 x i8]* @__CompressedAssemblyDescriptor_data_42, i32 0, i32 0); data
	}, 
	; 43
	%struct.CompressedAssemblyDescriptor {
		i32 52224, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([52224 x i8], [52224 x i8]* @__CompressedAssemblyDescriptor_data_43, i32 0, i32 0); data
	}, 
	; 44
	%struct.CompressedAssemblyDescriptor {
		i32 29184, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([29184 x i8], [29184 x i8]* @__CompressedAssemblyDescriptor_data_44, i32 0, i32 0); data
	}, 
	; 45
	%struct.CompressedAssemblyDescriptor {
		i32 1191424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1191424 x i8], [1191424 x i8]* @__CompressedAssemblyDescriptor_data_45, i32 0, i32 0); data
	}, 
	; 46
	%struct.CompressedAssemblyDescriptor {
		i32 12800, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([12800 x i8], [12800 x i8]* @__CompressedAssemblyDescriptor_data_46, i32 0, i32 0); data
	}, 
	; 47
	%struct.CompressedAssemblyDescriptor {
		i32 798720, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([798720 x i8], [798720 x i8]* @__CompressedAssemblyDescriptor_data_47, i32 0, i32 0); data
	}, 
	; 48
	%struct.CompressedAssemblyDescriptor {
		i32 132608, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([132608 x i8], [132608 x i8]* @__CompressedAssemblyDescriptor_data_48, i32 0, i32 0); data
	}, 
	; 49
	%struct.CompressedAssemblyDescriptor {
		i32 102400, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([102400 x i8], [102400 x i8]* @__CompressedAssemblyDescriptor_data_49, i32 0, i32 0); data
	}, 
	; 50
	%struct.CompressedAssemblyDescriptor {
		i32 169472, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([169472 x i8], [169472 x i8]* @__CompressedAssemblyDescriptor_data_50, i32 0, i32 0); data
	}, 
	; 51
	%struct.CompressedAssemblyDescriptor {
		i32 2115072, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2115072 x i8], [2115072 x i8]* @__CompressedAssemblyDescriptor_data_51, i32 0, i32 0); data
	}, 
	; 52
	%struct.CompressedAssemblyDescriptor {
		i32 402944, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([402944 x i8], [402944 x i8]* @__CompressedAssemblyDescriptor_data_52, i32 0, i32 0); data
	}, 
	; 53
	%struct.CompressedAssemblyDescriptor {
		i32 2255360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2255360 x i8], [2255360 x i8]* @__CompressedAssemblyDescriptor_data_53, i32 0, i32 0); data
	}
], align 4; end of 'compressed_assembly_descriptors' array


; compressed_assemblies
@compressed_assemblies = local_unnamed_addr global %struct.CompressedAssemblies {
	i32 54, ; count
	%struct.CompressedAssemblyDescriptor* getelementptr inbounds ([54 x %struct.CompressedAssemblyDescriptor], [54 x %struct.CompressedAssemblyDescriptor]* @compressed_assembly_descriptors, i32 0, i32 0); descriptors
}, align 4


!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ a8a26c7b003e2524cc98acb2c2ffc2ddea0f6692"}

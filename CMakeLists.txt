option(BUILD_SHARED_LIBS "Build Shared Libraries")
option(BUILD_EXTERNAL "Build external dependencies in /External" ON)
option(BUILD_WERROR "Enable warnings as errors (default is OFF)" OFF)
option(ENABLE_SPVREMAPPER "Enables building of SPVRemapper" ON)
option(ENABLE_GLSLANG_BINARIES "Builds glslang and spirv-remap" ON)
option(ENABLE_HLSL "Enables HLSL input support" ON)
option(ENABLE_RTTI "Enables RTTI")
option(ENABLE_EXCEPTIONS "Enables Exceptions")
option(ENABLE_OPT "Enables spirv-opt capability if present" ON)
option(ALLOW_EXTERNAL_SPIRV_TOOLS "Allows to build against installed SPIRV-Tools-opt")
option(GLSLANG_TESTS "Enable glslang testing")
import os
import shutil
import subprocess
import platform
import argparse
import runpy


# should not run as submodule
if __name__ != "__main__":
    exit()

parser = argparse.ArgumentParser(description='Build DirectXShaderCompiler from source with changes')
parser.add_argument('-T', '--target', dest='target', required=False, help='Specify the target platform- options are (Windows, Linux, Darwin)')
args = parser.parse_args()

source_directory = os.getcwd()

# modified version of DirectXShaderCompiler
dx_repo = "https://github.com/KhronosGroup/glslang.git"

gl_directory = os.path.join(source_directory, 'Glslang')
gl_source_dir = os.path.join(gl_directory, 'Source')
gl_build_dir = os.path.join(gl_directory, 'Build')


def check_tool(tool_name):
    try:
        # Try running the tool with the "--version" flag to check if it's installed
        subprocess.run([tool_name, '--version'], stdout=subprocess.PIPE, stderr=subprocess.PIPE, check=True)
        return True
    except subprocess.CalledProcessError:
        return False

# ensure CMake installation
if not check_tool('cmake'):
    print("CMake is required to build glslang. Make sure the CMake build system is downloaded and on the PATH")
    exit()

os.makedirs(gl_directory, exist_ok=True)

def git_clone_or_pull(repo_url, destination_dir):
    # check if the destination directory is nonexistent or empty
    is_empty = not os.path.exists(destination_dir) or not any(os.scandir(destination_dir))

    if is_empty:
        # clone repo if the directory is empty
        subprocess.run(['git', 'clone', '--recursive', repo_url, destination_dir], check=True)
    else:
        # update directory if not empty
        subprocess.run(['git', 'pull'], cwd=destination_dir, check=True)

def add_cmake_define(args_array, define_name, define_value):
    args_array.append('-D' + define_name + '=' + define_value)


# clone original dxc repository
git_clone_or_pull(dx_repo, gl_source_dir)

# create build directory
os.makedirs(gl_build_dir, exist_ok=True)

subprocess.run(['python3', './update_glslang_sources.py'], cwd=gl_source_dir, check=True)

cmake_args = ['cmake']

add_cmake_define(cmake_args, 'ENABLE_HLSL', 'ON')
add_cmake_define(cmake_args, 'CMAKE_BUILD_TYPE', 'Release')
add_cmake_define(cmake_args, 'BUILD_SHARED_LIBS', 'ON')
add_cmake_define(cmake_args, 'CMAKE_INSTALL_PREFIX', gl_build_dir+'/install')

cmake_args.append('../Source')


subprocess.run(cmake_args, cwd=gl_build_dir, check=True)

subprocess.run(['make', '-j8', 'install'], cwd=gl_build_dir)

#subprocess.run(['cmake', '--build', '.', '--config', 'Release', '--target', 'install'], cwd=gl_build_dir)
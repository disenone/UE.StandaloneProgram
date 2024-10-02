# -*- coding: utf-8 -*-
import argparse
import os


def Format(text, str_template):
    for key, val in str_template.items():
        text = text.replace(key, val)
    return text


def GenerateFiles(args):
    str_template = {
        '{_ProgramName_}': args.name,
    }

    template_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'template')
    output_path = args.output

    for root, dirs, files in os.walk(template_path):
        for file in files:
            output_file_name = Format(file, str_template)
            # print('Creating [%s]' % (output_file_name, ))
            output_folder = os.path.join(output_path, os.path.relpath(root, template_path))
            os.makedirs(output_folder, exist_ok=True)
            output_file_path = os.path.join(output_folder, output_file_name)
            with open(os.path.join(root, file), 'r', encoding='utf-8') as input_file:
                with open(output_file_path, 'w', encoding='utf-8') as output_file:
                    input_text = input_file.read()
                    output_text = Format(input_text, str_template)
                    output_file.write(output_text)
            print('Write [%s]' % (output_file_path, ))


def ParseArgs():
    parser = argparse.ArgumentParser(description='Create Standalone Program In UE5')
    parser.add_argument('name', type=str, help='Program Name')
    parser.add_argument('-uproject', type=str, default='', help='.uproject file path, Program will output to uproject_path/Source/Programs')
    parser.add_argument('-output', type=str, default='', help='specific output path')
    # todo: set ue engine path
    # todo: find ue engine path from registry

    args = parser.parse_args()

    output = None
    if args.uproject:
        if not args.uproject.endswith('.uproject'):
            print('Error: [%s] is not uproject file' % args.uproject)
            return parser.print_help()

        if not os.path.exists(args.uproject):
            print('Error: uproject file [%s] not exists' % args.uproject)
            return parser.print_help()

        output = os.path.join(os.path.dirname(os.path.abspath(args.uproject)), 'Source', 'Programs')

    elif not args.output:
        output = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'output')

    if output:
        args.output = os.path.join(output, args.name)

    if os.path.exists(args.output) and not os.path.isdir(args.output):
        print('Error: output [%s] is not directory' % args.output)
        return parser.print_help()

    return args


def Main():
    args = ParseArgs()
    if not args:
        return

    print('Creating [%s] into [%s]' % (args.name, args.output))

    # os.makedirs(os.path.join(args.output, args.name), exist_ok=True)
    GenerateFiles(args)


if __name__ == '__main__':
    Main()

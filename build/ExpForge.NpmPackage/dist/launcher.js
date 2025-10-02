#!/usr/bin/env node
const { spawn } = require('child_process');
const path = require('path');
const os = require('os');

let exePath;

switch (os.platform()) {
  case 'win32':
        exePath = path.join(__dirname, 'win-x64', 'ExpForge.Presentation.exe');
    break;
  case 'linux':
        exePath = path.join(__dirname, 'linux-x64', 'ExpForge.Presentation');
    break;
  case 'darwin':
        exePath = path.join(__dirname, os.arch() === 'arm64' ? 'osx-arm64' : 'osx-x64', 'ExpForge.Presentation');
    break;
  default:
    console.error(`Unsupported platform: ${os.platform()}`);
    process.exit(1);
}

// Executa o binário com todos os argumentos passados
const child = spawn(exePath, process.argv.slice(2), { stdio: 'inherit' });

// Retorna o mesmo código de saída do executável
child.on('exit', code => process.exit(code));

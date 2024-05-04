import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import vue from '@vitejs/plugin-vue'
import { env } from 'process';

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "internetshopping.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:5001';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        plugin()
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/product': {
                target: 'https://localhost:7146/Products',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/product/, '')
            },
            '^/authorizations': {
                target: 'https://localhost:7146/Authorization',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/authorizations/, '')
            },
            '^/customers': {
                target: 'https://localhost:7146/Customers',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/customers/, '')
            },
            '^/orders': {
                target: 'https://localhost:7146/Orders',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/orders/, '')
            },
            '^/order_item': {
                target: 'https://localhost:7146/Order_item',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/order_item/, '')
            },
            '^/stockroom': {
                target: 'https://localhost:7146/Stockroom',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/stockroom/, '')
            },
            '^/category': {
                target: 'https://localhost:7146/Category',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/category/, '')
            },
            '^/supplier': {
                target: 'https://localhost:7146/Supplier',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/supplier/, '')
            },
        },
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
})

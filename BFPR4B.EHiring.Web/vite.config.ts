import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "bfpr4b.ehiring.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

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

const target = env["services__bfpr4b.ehiring-server__https__0"] ?? 'https://localhost:7222';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
  server: {
        proxy: {
              '^/api': {
                target: target, // Use the URL injected by Aspire
                changeOrigin: true,      // Required for proxying to a different origin
                secure: false,           // Set to true if your API is using a valid, trusted HTTPS certificate.
                // For dev-certs in local dev, false is often needed.
                // This rewrite ensures that /api/your-endpoint becomes /your-endpoint for the backend.
                // Adjust if your backend explicitly expects /api/ in its routes (e.g., [Route("api/[controller]")]).
                rewrite: (path) => path.replace(/^\/api/, ''),
              },
        },
        port: parseInt(env.DEV_SERVER_PORT || '56539'),
        strictPort: true, // Optional: ensures Vite only uses this port
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
})

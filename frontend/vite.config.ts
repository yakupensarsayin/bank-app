import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import mkcert from 'vite-plugin-mkcert'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), mkcert()],
  resolve: {
    alias:{
      "@img": "/src/assets/img",
      "@css": "/src/assets/css",
      "@components": "/src/components",
      "@pages": "/src/pages"
    }
  }
})

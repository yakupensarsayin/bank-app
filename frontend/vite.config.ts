import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias:{
      "@img": "/src/assets/img",
      "@css": "/src/assets/css",
      "@components": "/src/components",
      "@pages": "/src/pages"
    }
  }
})

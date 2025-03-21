const { env } = require('process');

const target = "https://localhost:7225";
const PROXY_CONFIG = [
  {
    context: ["/api"],
    target: target,
    secure: false,
    changeOrigin: true
  }
];
module.exports = PROXY_CONFIG;


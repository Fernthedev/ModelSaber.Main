const webpack = require("webpack");
const path = require("path");

module.exports = {
    entry: path.resolve(__dirname, "./src/index.tsx"),
    module: {
        rules: [
            {
                test: /\.(ts|tsx)?$/,
                exclude: /node_modules/,
                use: ["ts-loader"],
            },
            {
                test: /\.(js|jsx)?$/,
                exclude: /node_modules/,
                use: ["babel-loader"],
            },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    "style-loader",
                    {
                        loader: "css-loader",
                        options: {
                            sourceMap: true
                        }
                    },
                    {
                        loader: "sass-loader",
                        options: {
                            sourceMap: true
                        }
                    },
                ],
            },
        ],
    },
    resolve: {
        extensions: ["*", ".js", ".jsx", ".ts", ".tsx"],
    },
    output: {
        path: path.resolve(__dirname, "./dist"),
        filename: "bundle.js",
    },
    plugins: [new webpack.HotModuleReplacementPlugin()],
    devServer: {
        static: path.resolve(__dirname, "./dist"),
        hot: true,
    },
    devtool: "inline-source-map"
};
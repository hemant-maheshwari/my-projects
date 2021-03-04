const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');

module.exports = {
  entry: './src/main.ts',
  resolve: {
    extensions: ['.ts', '.js'],
    alias: {
      '@': path.resolve(__dirname, 'src/app/'),
    }
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: ['ts-loader', 'angular2-template-loader']
      },
      {
        test: /\.html$/,
        use: 'html-loader'
      },
      {
        test: /\.less$/,
        use: ['style-loader', 'css-loader', 'less-loader']
      },

      // workaround for warning: System.import() is deprecated and will be removed soon. Use import() instead.
      {
        test: /[\/\\]@angular[\/\\].+\.js$/,
        parser: { system: true }
      }
    ]
  },
  plugins: [
    new HtmlWebpackPlugin({ template: './src/index.html' }),
    new webpack.DefinePlugin({
      // global app config object
      config: JSON.stringify({
        apiUrl: 'http://localhost:8080/api/v1'
      })
    }),

    // workaround for warning: Critical dependency: the request of a dependency is an expression
    new webpack.ContextReplacementPlugin(
      /\@angular(\\|\/)core(\\|\/)fesm5/,
      path.resolve(__dirname, 'src')
    )
  ],
  optimization: {
    splitChunks: {
      chunks: 'all',
    },
    runtimeChunk: true
  },
  devServer: {
    historyApiFallback: true
  }
}
// Webpack 4 is used to compile and bundle all the project files so they're ready to be loaded into a
// browser, it does this with the help of loaders and plugins that are configured in the webpack.config.js file.
// For more info about webpack check out the webpack docs.
//
// This is a fairly basic webpack.config.js for bundling an Angular 8 application, it:
//
// compiles Angular TypeScript files using ts-loader.
// loads angular templates with the angular2-template-loader and html-loader.
// converts LESS files into CSS and loads them into the application with the style-loader, css-loader and less-loader.
// injects the bundled scripts into the body of the index.html page using the HtmlWebpackPlugin.
// defines a global config object with the plugin webpack.DefinePlugin.
// A path alias '@' is configured in the webpack.config.js and the tsconfig.json that maps to the '/src/app' directory.
// This allows imports to be relative to the '/src/app' folder by prefixing the import path with '@',
// removing the need to use long relative paths like import MyComponent from '../../../MyComponent'.
//
// It also includes a couple of workarounds to prevent the following warnings from appearing in the
// console when running the app:
// "System.import() is deprecated and will be removed soon. Use import() instead."
// and "Critical dependency: the request of a dependency is an expression".
// The warnings themselves are harmless, this is just to prevent them from displaying.

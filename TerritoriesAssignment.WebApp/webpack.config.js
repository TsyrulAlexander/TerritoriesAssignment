const path = require('path');
const webpack = require('webpack');
module.exports = {
	mode: 'development',
	optimization: {
		minimize: false
	},
	entry: {
		'polyfills': './ClientApp/polyfills.ts',
		'app': './ClientApp/main.ts'
		//'styles': path.resolve(__dirname, './wwwroot/styles/style.css')
	},
	output: {
		path: path.resolve(__dirname, './wwwroot/dist'),
		publicPath: '/dist/',
		filename: "[name].js"
	},
	resolve: {
		extensions: ['.ts', '.js']
	},
	module: {
		rules: [
			{
				test: /\.ts$/,
				use: [{
						loader: 'awesome-typescript-loader',
						options: { configFileName: path.resolve(__dirname, 'tsconfig.json') }
					},
					'angular2-template-loader'
				]
			},{
				test: /\.html$/,
				loader: 'html-loader'
			}, {
				test: /\.css$/,
                loaders: ['to-string-loader', 'css-loader']
			}, {
				test: /\.(scss|sass)$/,
				use: [
					'to-string-loader',
					{
						loader: 'css-loader',
						options: {
							sourceMap: true
						}
					},
					{
						loader: 'sass-loader',
						options: {
							sourceMap: true
						}
					}
				],
				include: path.resolve(__dirname, 'ClientApp')
			}
		]
	},
	plugins: [
		new webpack.ContextReplacementPlugin(
				/angular(\\|\/)core/,
				path.resolve(__dirname, 'ClientApp'), // каталог с исходными файлами
				{} // карта маршрутов
		)
	]
};
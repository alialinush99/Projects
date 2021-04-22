<?php

use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});



Route::resource('movies', 'MovieController');
Route::resource('posts','PostController');


Auth::routes();


Route::resource('users', 'UserController');

Route::get('/home', 'HomeController@index');

Route::group(['prefix' => 'admin', 'middleware' => 'can:admin'], function () {
    Route::group(['prefix' => 'users'], function () {
        Route::get('/', 'Admin\UserController@index')->name('admin.users.index');
        Route::get('/{user}/edit', 'Admin\UserController@edit')->name('admin.users.edit');
        Route::get('/create', 'Admin\UserController@create')->name('admin.users.create');
        Route::delete('/{user}', 'Admin\UserController@destroy')->name('admin.users.delete');
        Route::patch('/{user}', 'Admin\UserController@update')->name('admin.users.update');
        Route::post('/', 'Admin\UserController@store')->name('admin.users.store');
    });
    
});


<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use App\User;
use Illuminate\Http\Request;

class UserController extends Controller
{

    public function index()
    {
        $users = User::all();

        return view('admin.users.index', [
            'users' => $users
        ]);
    }

    public function create()
    {
        return view('admin.users.create');
    }

    public function edit(User $user)
    {
        return view('admin.users.update', compact('user'));
    }
    
    public function destroy(User $user)
    {
        $user->delete();
        
        return back()->with('success', 'User deleted!');
    }

    public function update(Request $request, $id) //for updating the profile of the user
    {
        $request->validate([
            'name' => 'required',
            'email' => 'required',
            'password' => 'required|min:4|'

        ]);

        $user = User::find($id);
        $user->name =  $request->get('name'); //makes the update
        $user->email = $request->get('email');
        $user->password = bcrypt($request->get('password'));

        $user->save();


        return back()->with('success', 'User updated!');
    }
  

    public function store(Request $request)
    {
        $request->validate([
            'name' => 'required',
            'email' => 'required',
            'password' => 'required',
        ]);

        $user = new User([
            'name' => $request->get('name'),
            'email' => $request->get('email'),
            'password' => $request->get('password'),
        ]);
        $user->save();

        return back()->with('success', 'User was successfully created!');
    }
}
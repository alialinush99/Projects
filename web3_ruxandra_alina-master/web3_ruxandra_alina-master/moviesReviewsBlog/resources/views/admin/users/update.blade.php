
@extends('layouts.app')
@section('content')

<div class="row">
    <div class="col-md-12">
    <h3 class="display-4 mb-5 mt-4 text-center ">{{ $user->name }} Details</h3> 
<div>

<form method="post" action="{{ route('users.update', $user->id) }}" enctype="multipart/form-data"> 
        @method('PATCH') 
        @csrf

<div class="form-group">
            <label for="title">Name:</label>
              <input type="text" class="form-control" name="name" value= "{{ $user->name}}">
          </div>

          <div class="form-group">
              <label for="text">Email:</label>
              <input type="text" class="form-control" name="email" value= "{{$user->email}}" >
          </div >

          <div class="form-group">
              <label for="text">Password:</label>
              <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password" name="password"/>
          </div >

          <button type="submit" class="btn btn-success">Update</button>
</form>
          </div>
    </div>
</div>

@endsection
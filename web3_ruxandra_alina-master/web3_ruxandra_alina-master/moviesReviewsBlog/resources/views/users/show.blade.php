@extends('layouts.app')
@section('content')

<h3 class="display-4 mb-5 text-center mt-4">{{ $user->name }} Details</h3>

<div class="text-center mb-5">
<img src="{{asset(auth()->user()->getAvatar())}}" alt="Profile Avatar" width="150" height="150" style="border-radius:50%">

</div>


<div class="card text-center">
  <div class="card-body">
    <h5 class="card-title">Details:</h5>
    
    <p class="card-text">
      <div>
        <div class="mb-2">
          <p class="font-weight-bold d-inline">Name:</p>
          <p class="font-weight-light d-inline">{{ $user->name }}</p>
        </div>
        <div>
          <p class="font-weight-bold d-inline">Email:</p>
          <p class="font-weight-light d-inline">{{ $user->email }}</p>
        </div>
      </div>
    </p>


    <a style="margin: 19px;" href="{{  route('users.edit', $user->id) }}" class="btn btn-primary">Edit</a>
  </div>
</div>


@endsection
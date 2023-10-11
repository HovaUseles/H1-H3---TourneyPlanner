import 'dart:io';
import 'package:flutter/services.dart';

Future<HttpClient> setHttpClient() async {
  ByteData trustedCertificate =
      await rootBundle.load('assets/localhost+3-client.p12');
  ByteData certificateChain =
      await rootBundle.load('assets/localhost+3.pem');
  ByteData privateKey =
      await rootBundle.load('assets/localhost+3-key.pem');
  List<int> trustedCertificateBytes = trustedCertificate.buffer.asUint8List(
      trustedCertificate.offsetInBytes, trustedCertificate.lengthInBytes);
  List<int> certificateChainBytes = certificateChain.buffer.asUint8List(
      certificateChain.offsetInBytes, certificateChain.lengthInBytes);
  List<int> privateKeyBytes = privateKey.buffer
      .asUint8List(privateKey.offsetInBytes, privateKey.lengthInBytes);
  SecurityContext context = SecurityContext();
  context.setTrustedCertificatesBytes(trustedCertificateBytes,
      password: 'changeit');
  context.useCertificateChainBytes(certificateChainBytes, password: 'changeit');
  context.usePrivateKeyBytes(privateKeyBytes, password: 'changeit');
  return HttpClient(context: context)
   ..badCertificateCallback =
       (X509Certificate cert, String host, int port) => true;
}
